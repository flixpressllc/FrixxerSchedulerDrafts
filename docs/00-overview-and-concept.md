# Frixxer Overview and Concepts

This is NOT to be confused with FrixxerNews as this new Frixxer System is an entirely new and unrelated system.

## The End Product

The most basic produced unit at Frixxer is a `Presentation` of a succession of videos along with other content such as ads, weather bits,
scroll text, and more. An establishment would place a monitor on display to the public which will play `Presentation`s in succession.

Each `Presentation` is a single linear list of video URLs to be played in succession throughout its execution. Each video in
the `Presentation` has `Tags`, or keywords associated with it (video, not the whole presentation), which will determine, shortly
before execution time, which ads and other supporting content will be displayed while it's playing. We will later discuss the intricacies
of how a `Presentation` is created, scheduled, and displayed.

Unlike our previous projects, a `Presentation` does not call for rendering a single final video, thus is not associated with a final video
URL. There will not be requests made to a rendering engine, especially on "go live", or any FFMPEG manipulation to put together the entire
`Presentation`.

## How a Presentation is Created
A user creates a `Presentation` via a UI, most likely web browser-based. In the editing UI, the user gets to choose which videos to play 
in succession in the main content area. Choice of video may be a direct upload (for which the upload will immediately start upon choosing a 
file, and a video URL returned, which would then be saved to the `Presentation`), or stock video that we will have them choose from 
(which will be a stock video service that we need to write and administratively populate). For each main content video specified, the user
specifies tags, or keywords, that would drive which ads will show up (if there is an ad space) while the main video is playing.

There arises the question of when the `Presentation` will actually air. It seems natural to store the information of when the `Presentation` 
will air with the presentation itself, but we will address that in the scheduling aspect of this system. So, a `Presentation` will not be 
associated with specific dates and times. A `Presentation` will be concerned only with its content and other information that 
will drive its dynamic parts for when it actually airs. This way, the `Presentation` may be scheduled at will at any time, and the resulting
video will have up-to-date dynamic content by the time it airs.

## Channels
A `Channel` in the Frixxer system is analogous to, or even exactly the same concept as a TV channel. A `Channel` inherently has a single 
timeline where `Presentations` will eventually play in succession once scheduled. A single company may own multiple `Channel`s for which
they may schedule `Presentations` to air. This then enables the company to set up multiple monitors, one for each `Channel`, that will
display its scheduled programming.

The current targeted manifestation of a `Channel`, at this point, is a client-side web page. This means that the web page will be shown 
with the `Presentation`s playing in it. The JS itself will expect to read a .json file at a given time and from that .json file,
will obtain the info needed to play the `Presentation`. As to how, when, and where such a .json file is created, we will address that in
a later technical section.

## Presentation Templates
While creators of a `Presentation` gets to define main video content and meta-data that drives a presentation's dynamic parts, they do not
control the physical and aesthetic aspect of a `Presentation`. While they can control which videos play in succession, they do not get to
define where on the whole screen that main video will play or how large that rectangular area will be, nor do they get to decide location and
size of the other content. They also do not control the length of the presentation, which is slated at 5 minutes all across the
board. They also do not control what types of content will appear in the video, for example, they don't get to decide whether or not to
have an ad space, let alone position it on the screen.

The physicality of a `Presentation` is defined by a `PresentationTemplate`, which we as top-level administrators will create. A 
`PresentationTemplate` dictates how many of each type of rectangular content areas will be on the entire presentation. It sounds like, 
then, that before creating a `Presentation`, the user needs to first choose a `PresentationTemplate`. This way, the UI will automatically
reflect, for example, that they get to fill a main content area, specify content for 3 scrolls, one ad space, and one static image space. 
However, this is NOT the workflow although it feels natural (as we have done in all other template-based content creation systems). We will
address this issue in the next section.

## Presentation Templates vs. Channels vs. Web Page vs. Presentations
Recall that a `Channel`'s actual concrete manifestation is a client-side web page. This exactly means that the rectangular areas where 
content of a `Presentation` will show up are hard-coded in HTML and CSS, both size and position, along with any fixed background images 
and colors. Since a `PresentationTemplate` describes what content areas are to be shown in a `Presentation`, and the client-side web page
hard-codes the size and position of rectangular areas for each of the content areas, the `Channel`, the `PresentationTemplate`,
and the hosting web page are tightly coupled, thus all `Presentation`s that are to play on a `Channel` will be stuck to a single physical 
layout, thus, physically look identical.

For top-level administration, this means that we must manually ensure that the rectangular areas we define in a `PresentationTemplate` will
match those in the corresponding web pages. So, when we create a `PresentationTemplate`, we must also manually markup and design the web
page to contain the same exact rectangular areas we've defined in the `PresentationTemplate`. If we change something in either `PresentationTemplate`
or web page, we must manually update the other so that both will be synched.

## Scheduling
So far, a user will have created multiple `Presentation`s, multiple on each `Channel` they own. However, we still need to know when exactly the
`Presentation` will play. This is where scheduling comes in. At this time, scheduling is NOT fully finalized.

The following is mere speculation:

There are talks of 30-minute blocks which may likely be a list of references to `Presentation`s in order that total 30 minutes of play time.
This approach seems hierarchical because there's a natural drive to then create block for an entire day, which would be a collection of 48 
30-minute blocks to fill the entire 24-hour period. Then there can be a week block, which contains 7 references in order to specific day blocks, and so 
on. So far, we have NOT specified *exactly when* presentations will be aired. We have only addressed when presentations will play relative to
each other within a span of time.

## Execution
At this point, let us assume that a user has created enough `Presentation`s to cover an entire week's period of content for one channel. We
have a web page that reads from a .json file that will eventually contain complete instructions for what the web page will present. We
do not know when that .json file will be created or overwritten, what creates it, and its exact data structure. We know that it will NOT
be the exact .json for any given `Presentation` created by a user, since those are merely descriptors, not the exact statement of what
people will actually see when it airs!

As discussed, there will be a continuously-running console application that will be responsible for generating and storing the .json file
that the web page will "render". That console application will need to know what `Channel` it's for so that it will pull only 
`Presentation`s of that particular `Channel`. Its basic query is to obtain the next x-minutes worth of `Presentation`s starting at a
particular near-future time. For each `Presentation` returned in that query, we will need to fetch some ads (if it applies to the 
`Presentation`) via an API call to an ads service. So, yes, for each API service that this console app will call, it needs to keep track 
of all of multiple credentials. This console app may perform some downloading of video files (main content area), so we need to provide 
some time between querying and airing time to ensure that all videos are downloaded before airing.

## Clientele
This has not been discussed, though we know if someone creates `presentation`s, someone will need to be registered to the system. Tentatively,
we as top-level administrators manually create users. However, the natural inclination is that someone will want to register on behalf of
a company, which allows multiple users of a company to be able to simultaneously create and manage content.

How these clients will pay for our service hasn't been discussed. Payment providers, and even plans, contracts, and allowances have not
been discussed.
