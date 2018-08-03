# Frixxer Overview and Concepts

This is NOT to be confused with FrixxerNews as this new Frixxer System is an entirely new and unrelated system.

## The End Product
An establishment would like to place a monitor on display to the public which will play videos in succession. These videos are not just
individual .mp4 files that already exist in some computer or server just waiting to be played. These videos contain different parts, such
as the main content, scrolls, ad space, and more. While which content areas to play at specific times are pre-determined, the other screen
sections are dynamically pulled and the whole video becomes ready a some short time before actually airing or broadcasting it. These videos
are said to be strictly five minutes in length, though that may easily vary in the near future.

## How a Presentation is Created
We shall refer to a video with main content areas and other accompanying content areas as a presentation. A user creates a presentation
via a UI, most likely a web browser-based UI. In the presentation editing UI, the user gets to choose which videos to play in succession
in the main content area. Choice of video may be a direct upload (for which the upload will immediately start upon choosing a file, and a
video URL obtained), or stock video that we will have them choose from (which will be another stock video service that we need to write
and administratively populate). For each main video specified, the user specifies tags, or keywords, that would drive which ads
will show up (if there is an ad space) while the main video is playing.

There arises the question of when the presentation will actually air. It seems natural to store the information of when the presentation 
will air with the presentation itself, but we will address that in the scheduling aspect of this system. So, a presentation will not be 
associated with specific dates and times. As it stands, a presentation will be concerned only with its content and other information that 
will drive its dynamic parts for when it actually airs. This way, the presentation may be scheduled at will at any time, and the resulting
video will have up-to-date dynamic content by the time it airs.

It is important to address this following gotcha at this time: There will NOT be a quick rendering and/or concatenation of any videos

## Channels
A channel in the Frixxer system is analogous to, or even exactly the same concept as a TV channel. A channel inherently has a single timeline
where presentations will eventually play in succession once scheduled. A single company may own multiple channels for which they may schedule
presentations to play. This then enables the company to set up multiple monitors, one for each channel, that will display its scheduled
programming.

The current targeted manifestation of a channel, at this point, is a client-side web page. This means that the web page will be shown 
with the presentations playing in it. The JS itself will expect to read a .json file at a given time and from that .json file,
will obtain the info needed to play the presentation. As to how, when, and where such a .json file is created, we will address that in
a later technical section.

## Presentation Templates
While creators of a presentation gets to define main video content and meta-data that drives a presentation's dynamic parts, they do not
control the physical and aesthetic aspect of the presentation. While they can control which videos play in succession, they do not get to
define where on the whole screen that main video will play or how large that rectangular area will be, nor do they get to decide location and
size of the other rectangular areas. They also do not control the length of the presentation, which is slated at 5 minutes all across the
board. They also do not control what types of content will appear in the video, for example, they don't get to decide whether or not to
have an ad space, let alone position it on the screen.

The physicality of a presentation is defined by a presentation template, which we as top-level administrators will create. A presentation
template dictates how many of each type of rectangular content areas will be on the entire presentation. It sounds like, then, that before
creating a presentation, the user needs to first choose a presentation template. This way, the UI will automatically reflect, for example,
that they get to fill a main content area, specify content for 3 scrolls, one ad space, and one static image space. However, this is NOT
exactly the workflow although it feels natural (as we have done in all other template-based content creation systems). We will address this issue
in the next section.

## Presentation Templates vs. Channels vs. Presentations
Recall that a channel's actual concrete manifestation is a client-side web page. This exactly nmeans that the rectangular arease where videos 
of a presentation are hard-coded in HTML and CSS, both size and position. 

For top-level administration, this means that we must manually ensure that the rectangular areas we define in a presentation templates will
match those in the corresponding web pages. So, when we create a presentation template, we must also manually markup and design the web page to
contain the same exact rectangular areas we've defined in the presentation template. If we change something in either presentation template
or web page, we must manually update the other so that both will be synched.

Since the presentation template's web page is static, and a channel's manifestation is a web page, this means that a single channel is bound to
one and only one particular presentation template indefinitely. This means that when the user creates a presentation, they will need to choose a
channel, and since that channel is tightly bound to a single presentation template, all presentations of that channel will all look the same.
In other words, for the lifetime of the entire channel, AKA, as long as it plays its presentations, they will all look the same - sizes and
location of rectangular areas are constant indefinitely - and the colors and background images, as well as they're hard-coded on the hosting
web page.

## Scheduling
So far, a user has created multiple presentations, multiple on each channel they own. However, we still need to know when the presentation
will play. This is where scheduling comes in. At this time, scheduling is NOT fully finalized.

The following is mere speculation:

There are talks of 30-minute blocks which may likely be a list of references to presentations in order that total 30 minutes of play time. This
approach seems hierarchical because there's a natural drive to then create block for an entire day, which would be a collection of 48 30-minute 
blocks to fill the entire 24-hour period. Then there can be a week block, which contains 7 references in order to specific day blocks, and so 
on. So far, we have NOT specified *exactly when* presentations will be aired. We have only addressed when presentations will play relative to
each other within a span of time.

## Execution
At this point, let us assume that a user has created enough presentations to cover an entire week's period of content for one channel. We
have a web page that reads from a .json file that will eventually contain complete instructions for what the web page will present. We
do not know when that .json file will be created or overwritten, what creates it, and its exact data structure. We know that it will NOT
be the exact .json for any given presentation created by a user, since those are merely descriptors, not the exact statement of what
people will actually see when it airs!

As discussed, there will be a continuously-running console application that will be responsible for generating and storing the .json file
that the web page will "render". That console application will need to know what channel it's for so that it will only pull presentations
of that particular channel. It's basic query is to obtain the next x-minutes worth of presentations starting at a particular near-future
time. For each presentation returned in that query, we will need to fetch some ads (if it applies to the presentation) via an API call to
an ads service. So, yes, for each API service that this console app will call, it needs to keep track of all of multiple credentials. This
console app may perform some downloading of video files (main content area), so we need to provide some time between querying and airing
time to ensure that all videos are downloaded.

## Clientele
This has not been discussed, though we know if someone creates presentations, someone will need to be registered to the system. Tentatively,
we as top-level administrators manually create users. However, the natural inclination is that someone will want to register on behalf of
a company, which allows multiple users of a company to be able to simultaneously create and manage content.

How these clients will pay for our service hasn't been discussed. Payment providers, and even plans, contracts, and allowances have not
been discussed.
