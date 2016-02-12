# my-movie-db
<h4>To get this project up and running, you'll need to make a few tweaks to it.</h4>

<h3>Web.config</h3>

<p>First, in the <b>Web.config</b>, you'll need to update the connection string to point to your environment.</p>

<h3>SendGrid for email confirmation</h3>
<p>
  This project uses SendGrid to send emails to validate user email addresses. You'll need either need to update the <b>web.config</b> to   add credentials or un/comment some lines in the <b>Account Controller</b> to bypass email address validation.
</p>
<p>
<h4>To update the SendGrid credentials:</h4>
  <ul>
    <li>
      Create an AppSettingsSecrets.config file (or equivalent) containing an appSettings tag
			with "mailAccount" and "mailPassword" keys for your Sendgrid account and password.
    </li>
    <li>
    In <b>Web.config < appSettings></b>, replace <b>file="..\..\..\AppSettingsSecrets.config"</b> with an accurate path to your file.
    </li>
				<li>Light 'em up. Everything should work just fine.</li>
  </ul>
</p>
<p>
  <h4>To bypass email validation:</h4>
  <ul>
		<li>Go to the <b>Login HttpPost</b> method in <b>AccountController.cs</b></li>
		<li>
			From lines 76-90, there is code to validate emails (currently uncommented). Follow the commenting directions 
			in the code to bypass email validation.
		</li>
		<li>Go to the <b>Register HttpPost</b> method in <b>AccountController.cs</b>.<br /></li>
		<li>
			From lines 177-190, there is more code to either validate email addresses (uncommented) and to proceed without
			validating the email address. Follow the commenting directions to uncomment two lines and comment out a region.
		</li>
	</ul>
</p>

<h3>Things of Note Re:Security and Errors</h3>
<p>
	This project uses ASP.NET MVC canned security and validation. It's pretty decent, but as it stands it's not going to stop anybody angry and determined. SQL injection isn't a problem, and neither is javascript injection--but trying to test javascript/html injection will route you directly to the Yellow Screen Of Death because I haven't been able to figure out how to reroute site errors to friendly error pages. 
</p>

<p><b>
	Happy movie reviewing!<br/>
	--amfinegold
</b></p>
