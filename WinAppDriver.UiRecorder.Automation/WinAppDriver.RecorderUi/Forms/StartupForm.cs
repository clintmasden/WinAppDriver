using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinAppDriver.Generation.Client;
using WinAppDriver.Generation.Client.Models;
using WinAppDriver.Generation.Events.Hook.Models;
using WinAppDriver.Generation.PlaybackEvents.Extensions;
using WinAppDriver.Generation.PlaybackEvents.Models;
using WinAppDriver.Generation.UiEvents.Models;
using WinAppDriver.RecorderUi.States;

namespace WinAppDriver.RecorderUi
{
    /// <summary>
    /// Simple use case of recording / playing a recording using AutomationCOM / WinAppDriver
    /// </summary>
    /// <remarks>
    /// Highly suggest refactoring to WPF & MVVM pattern.
    /// </remarks>
    public partial class StartupForm : Form
    {
        private GenerationClient _generationClient { get; set; }

        private EventHandler<EventArgs> _generationClientHookProcedureEventHandler { get; set; }

        private EventHandler<UiEventEventArgs> _generationClientUiEventEventHandler { get; set; }

        private RecorderUiState _recorderUiState { get; set; }

        private readonly BindingList<UiEvent> _uiEvents = new BindingList<UiEvent>();

        public StartupForm()
        {
            InitializeComponent();

            SetupDataGridView();
            SetupDataBindings();
            SetupGenerationClient();
        }

        private void SetupDataGridView()
        {
            RecordedDataGridView.AutoGenerateColumns = true;
        }

        private void SetupDataBindings()
        {
            RecordedDataGridView.DataBindings.Add("DataSource", _uiEvents, String.Empty, true, DataSourceUpdateMode.OnPropertyChanged);
        }

        /// <summary>
        /// Setups Generation Client
        /// </summary>
        private void SetupGenerationClient()
        {
            if (_generationClient == null)
            {
                _generationClient = new GenerationClient(new GenerationClientSettings
                {
                    ProcessId = Process.GetCurrentProcess().Id,
                    AutomationTransactionTimeout = new TimeSpan(0, 1, 0),
                });

                _generationClientHookProcedureEventHandler = GenerationClientHookProcedure;
                _generationClient.GenerationHookProcedureEventHandler += _generationClientHookProcedureEventHandler;

                _generationClientUiEventEventHandler = GenerationClientUiEvent;
                _generationClient.GenerationUiEventEventHandler += _generationClientUiEventEventHandler;
            }
        }

        /// <summary>
        /// Sets Ui / State when the "pause" key on the keyboard is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerationClientHookProcedure(object sender, EventArgs e)
        {
            if (_generationClient.IsPaused)
            {
                SetUiForRecorderUiStateIsStopped();
            }
            else
            {
                SetUiForRecorderUiStateIsRecording();
            }
        }

        private void SetUiForRecorderUiStateIsStopped()
        {
            _recorderUiState = RecorderUiState.IsStopped;
            RecordButton.Text = $"Record";
        }

        private void SetUiForRecorderUiStateIsRecording()
        {
            _recorderUiState = RecorderUiState.IsRecording;
            RecordButton.Text = $"Stop";
            StartButton.Enabled = false;
        }

        private void SetUiForRecorderUiStateIsPlayback()
        {
            _recorderUiState = RecorderUiState.IsPlaying;
            StartButton.Enabled = false;
            RecordButton.Enabled = false;
        }

        private void SetUiForRecorderUiStateIsDefault()
        {
            _recorderUiState = RecorderUiState.IsDefault;
            RecordButton.Enabled = true;
            StartButton.Enabled = true;
        }

        /// <summary>
        /// Handler for new Events being created from WinAppDriver.Generation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerationClientUiEvent(object sender, UiEventEventArgs e)
        {
            _uiEvents.Add(e.UiEvent);

            RefreshRecorderDataGridView_YesThisIsAHack_UseTwoWayBinding();
        }

        private void RefreshRecorderDataGridView_YesThisIsAHack_UseTwoWayBinding()
        {
            RecordedDataGridView.DataSource = _uiEvents;

            RecordedDataGridView.Rows.OfType<DataGridViewRow>().Last().Selected = true;
            RecordedDataGridView.CurrentCell = RecordedDataGridView.Rows.OfType<DataGridViewRow>().Last().Cells.OfType<DataGridViewCell>().First();
        }

        /// <summary>
        /// IsPlaying functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void StartButton_Click(object sender, EventArgs e)
        {
            SetUiForRecorderUiStateIsPlayback();

            _generationClient.TerminateRecording();
            _generationClient.InitializePlayback();

            await ExecuteUiEvents();

            _generationClient.TerminatePlayback();
            SetUiForRecorderUiStateIsDefault();
        }

        /// <summary>
        /// Executes all UiEvents -> PlaybackEvents
        /// </summary>
        /// <remarks>
        /// simplified for your own usage
        /// </remarks>
        /// <returns></returns>
        private async Task ExecuteUiEvents()
        {
            await Task.Run(async () =>
            {
                for (int uiEventIndex = 0; uiEventIndex < _uiEvents.Count; uiEventIndex++)
                {
                    var uiEvent = _uiEvents[uiEventIndex];

                    try
                    {
                        new PlaybackEvent(uiEvent).Execute();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.ToString());
                        break;
                    }

                    RecordedDataGridView.BeginInvoke((MethodInvoker)delegate
                    {
                        RecordedDataGridView.CurrentCell = RecordedDataGridView.Rows[uiEventIndex].Cells.OfType<DataGridViewCell>().First();
                    });

                    await Task.Delay(50);
                }
            });
        }

        /// <summary>
        /// IsRecording / IsStopped functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecordButton_Click(object sender, EventArgs e)
        {
            if (_recorderUiState != RecorderUiState.IsRecording)
            {
                SetUiForRecorderUiStateIsRecording();
                _generationClient.InitializeRecording(new HookEventHandlerSettings { HasGraphicThreadLoop = true });
            }
            else if (_recorderUiState != RecorderUiState.IsStopped)
            {
                SetUiForRecorderUiStateIsStopped();
                SetUiForRecorderUiStateIsDefault();
                _generationClient.TerminateRecording();
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _uiEvents.Clear();
        }

        private void StartupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _generationClient.Terminate();
        }
    }
}