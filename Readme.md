#Usage:

1. In AXAML markup:	

   ```xaml
   xmlns:controls="using:Captcha"
	<controls:Captcha/>
   ```
   
2. Properties:

	```c#
	///The text entered by the user is necessary to verify the passage of the captcha
	public string InputUserText
	///Whether the captcha was passed or not, true - yes, false - no
	public bool IsVerified
	```
	
	
   
