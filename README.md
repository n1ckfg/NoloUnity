# Nolo Unity

Nolo Unity SDK 1.0<br>
Nolo Android Server 1.0.3<br>
Unity 2017.4<br>

### The Nolo kit:
<ul>
	<li>The head tracking marker contains the "brain" of the Nolo kit, and needs a wired USB connection to the host device.</li>
	<li>The controllers pair wirelessly with the head marker.</li>
	<li>The wireless base station works like a Vive Lighthouse--it only requires power; there's no data connection.</li>
</ul>

### Notes: 
<ul>
	<li>Although Nolo has Windows drivers (and can even be made to work with SteamVR), it's mainly intended for use with Android.</li>
	<li>For Android development, you don't have to install any of the Windows apps mentioned in the docs.</li>
	<li>You also don't want to install the Nolo Home Android app mentioned in the docs. That's an app store, and if you install it all your Android builds will ask for a cert you don't have.</li> 
	<li>Instead, install the DRM-free Android dev server apk in the tools folder.</li>
	<li>Although the docs say 4.4 KitKat is the recommended Android SDK, the examples use 7.0 Nougat.</li>
</ul>

### Pairing:<br>
http://forums.nolovr.com/discussion/493/pairing-issue-cant-pair-my-controllers<br>

### Calibrating:<br>
https://www.nolovr.com/OCGO

### Firmware update:<br>
https://www.reddit.com/r/NoloVR/comments/6nebl3/controllers_positional_tracking_are_reversed/dkyf2nc/<br>
If calibration repeatedly fails, old firmware may be to blame. Updater app in the tools folder; use at your own risk.<br>

<img src="./docs/images/cardboardsetting.png">
<img src="./docs/images/calibration.png">