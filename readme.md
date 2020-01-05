# Windows Application Driver
A refactor of WinAppDriver's Recorder + POC implementation of playback leveraging WinAppDriver's executable.


## Running

### Recording
1. Press Record [Click & Type as intended]
2. Press Stop
>Clear [Clears all created Events]

### Playback
1. Press Start  [Runs thru all created Events]


## Requirements
- WinAppDriver
- .NET Framework 4.7.2

## Samples
![](https://github.com/clintmasden/WinAppDriver/blob/poc.PlaybackImplementation/resources/calculator-sample.gif?raw=true)


> Leveraging Calculator

![](https://github.com/clintmasden/WinAppDriver/blob/poc.PlaybackImplementation/resources/wordpad-sample.gif?raw=true)

> Leveraging WordPad


## Change Log

### UiXPathLib
- Removed UiXPathLib (C Library)
- Engineered replacement leveraging automation COM within Generation project

### UiRecorder
- Moved WinAppDriver / automation flow into Generation project
-  Organized & refactored classes for normal usage patterns
-  Leveraged WinForms + GridView for POC samples


## Known Issues
- CAPS Lock with Keyboard Send Keys is currently not implemented
- NUM Lock with Keyboard Send Keys is currently not implemented