# ConnectionStatus
A connection status for wpf based on the Cortana UI and animations  

<img src="https://github.com/Peoky/BusyIndicator/blob/master/Images/Demo.gif" alt="Demo - Dash Loader" width="90%"></img>   

## Prerequisites:
<ul><li>.Net Framework 4.7.2 or higher</li></ul>  

## How to use:
1. Install the package via [NuGet](https://www.nuget.org/packages/ConnectionStatus/)    
<pre>Install-Package ConnectionStatus</pre>    

2. Add a reference to the library in your view  
<pre>xmlns:connectionstatus="clr-namespace:ConnectionStatus;assembly=ConnectionStatus"</pre>  

3. Add controller to your view:  
<pre><code>&lt;connectionstatus:CortanaControl Margin="12" Width="100" Height="100"/&gt;</code></pre>  

4. Create a public property named `ConnectionStatus` of type `ConnectionStatus.ConnectionStatus` and set its value to `Disconnected`, `Connecting`, `Verifying` or `Connected` (check demo for a working example).

##
If you like this, give it a * please.




