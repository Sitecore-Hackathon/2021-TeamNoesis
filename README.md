# Hackathon Submission Entry form


You can find a very good reference to Github flavoured markdown reference in [this cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet). If you want something a bit more WYSIWYG for editing then could use [StackEdit](https://stackedit.io/app) which provides a more user friendly interface for generating the Markdown code. Those of you who are [VS Code fans](https://code.visualstudio.com/docs/languages/markdown#_markdown-preview) can edit/preview directly in that interface too.

## Team name
TeamNoesis

## Category
The best enhancement to the Sitecore Admin (XP) for Content Editors & Marketers

## Description

### Real Time Page Views
**Module Purpose**

The purpose of this module is to provide the user with the information on how many users are viewing his website at that moment, also it provides with following information:

+ current page url 
+ url referal 
+ Country 
+ City 
+ Browser Information
+ Map of the various locations where there are active users. 

**What problem was solved**

So far Sitecore don't provide this kind of visualization of what's hapennig at that momment, this module is a step closer to be free of external website tracking tools and have all in one place. 

## Video link
⟹ Provide a video highlighing your Hackathon module submission and provide a link to the video. You can use any video hosting, file share or even upload the video to this repository. _Just remember to update the link below_

⟹ [Replace this Video link](#video-link)



## Pre-requisites and Dependencies

+ Sitecore 10.1 (rev.005207)


- List any dependencies
- Or other modules that must be installed
- Or services that must be enabled/configured

_Remove this subsection if your entry does not have any prerequisites other than Sitecore_

## Installation

+ Install Sitecore 10.1 (rev.005207) with SXA
+ Install the package with Control Panel wizard (Control Panel -> Install a package) [package](#)
+ Run the sql script on your server database
+ Change the Web.config replacing the entry "Content-Securuty-Policy" whith the following one:

> ``` <add name="Content-Security-Policy" value="default-src 'self' 'unsafe-inline' 'unsafe-eval' maps.googleapis.com; img-src 'self' data: maps.gstatic.com *.googleapis.com *.ggpht; style-src 'self' 'unsafe-inline' https://fonts.googleapis.com; font-src 'self' 'unsafe-inline' https://fonts.gstatic.com; upgrade-insecure-requests; block-all-mixed-content;" /> ```

+ Add the following entry to your connection string

> ``` <add name="analytics" connectionString="Data Source=localhost;Initial Catalog=sc101_Analytics;User ID=sc101;Password=2021TeamNoesis" /> ```
 
### Configuration
⟹ If there are any custom configuration that has to be set manually then remember to add all details here.

_Remove this subsection if your entry does not require any configuration that is not fully covered in the installation instructions already_

## Usage instructions
⟹ Provide documentation about your module, how do the users use your module, where are things located, what do the icons mean, are there any secret shortcuts etc.

Include screenshots where necessary. You can add images to the `./images` folder and then link to them from your documentation:

![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")

You can embed images of different formats too:

![Deal With It](docs/images/deal-with-it.gif?raw=true "Deal With It")

And you can embed external images too:

![Random](https://thiscatdoesnotexist.com/)

## Comments
If you'd like to make additional comments that is important for your module entry.
