# VookaRaylee

VR mod for Yooka-Laylee (Toybox & full version).

## Installing

1. Download the latest release from [the release page](https://github.com/Eusth/VookaRaylee/releases)
2. Extract the contents into your *Yooka-Laylee - Toybox* directory (right click on *Yooka-Laylee - Toybox* ➔ Properties ➔ Local Files ➔ Browse local files...)
3. Drag *Toybox64.exe* onto *IPA.exe*
4. Disable *Use Desktop Game Theatre while SteamVR is active* in the game properties

Now, whenever you start the game with SteamVR running, it will automatically switch into VR mode. (You will still get the warning that VR isn't supported -- just click it away.)

If you want to disable this behavior (e.g. to use the cinema mode), open the launch configuration dialog and add `--novr`. Likewise, you can force VR using `--vr`.

## Controls

In addition to the traditional game controls, you have a number of VR-specific commands:

|Key|Action|
|---|------|
|<kbd>Backspace</kbd>|Reset view|
|<kbd>Insert</kbd>|Apply image effects (Anti-Aliasing, etc.)|
|<kbd>F4</kbd>|Change GUI projection mode|

More are to be found in the file *vr_settings.xml*. Most notably, you can change the scale to your own liking and toggle the vignetting.

## Uninstalling

If you want to get rid of this mod:

1. Drag *Toybox64.exe* onto *IPA.exe* **while pressing <kbd>Alt</kbd>**.
2. Done