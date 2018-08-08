# Frixxer Overview and Concepts

This is NOT to be confused with FrixxerNews as this new Frixxer System is an entirely new and unrelated system.

## The End Product

The most basic produced unit at Frixxer is a `Presentation` of a succession of videos along with other content such as ads, weather bits,
scroll text, and more. An establishment would place a monitor on display to the public which will play `Presentation`s in succession.
Each presentation is slated to last for exactly 5 minutes, all across the board, though the `Presentation` data allows for any duration.

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
specifies tags, or keywords, that would drive which ads will show up while the main video is playing.

There arises the question of when the `Presentation` will actually air. It seems natural to store the information of when the `Presentation` 
will air, to the `Presentation` itself, but we will address that in the scheduling aspect of this system. So, a `Presentation` will not be 
associated with a specific date and time. A `Presentation` will be concerned only with its content and other information that 
will drive its dynamic parts for when it actually airs. This way, the `Presentation` may be scheduled at will at any time, and the resulting
video will have up-to-date dynamic content by the time it airs.

## Channels
A `Channel` in the Frixxer system is analogous to, or even exactly the same concept as a TV channel. A `Channel` inherently has a single 
timeline where `Presentations` will eventually play in succession once scheduled. A single company may own multiple `Channel`s for which
they may schedule `Presentations` to air. This then enables the company to set up multiple monitors, one for each `Channel`, that will
display its scheduled programming.

The current targeted manifestation of a `Channel`, at this point, is a client-side web page. This means that the web page will be shown 
with the `Presentation`s playing in it. The JS itself will expect to read a .json file at a given time and from that .json file,
will obtain the info needed to play the `Presentation`. We will later address the generation and details of the .json file that the
client-side web page will read in a later technical section.

## Presentation Templates
While creators of a `Presentation` get to define main video content and meta-data that drives a presentation's dynamic parts, they do not
control which types of content and how many of each that the `Presentation` will show. Yes, there will be the main video content rectangular
area and the ad rectangular area. They do not get to decide, though, how many scrolling texts to show, or how many additional content
rectangular areas to show (which could contain weather, trivia, stock market info, etc.). Scrolling text, additional content rectangular
areas, and total `Presentation` duration are decided by a `PresentationTemplate`.

`PresentationTemplate`s are created by top-level administrators. This implies that a user would first select a `PresentationTemplate`
from a list, and then their blank/starting `Presentation` would be returned to them to fill and then later save. However, that is not the
workflow of creating a `Presentation`. We will address this in a later section. 

Additionally, as `PresentationTemplate`s are responsible for defining rectangular areas for additional content, scrolls, and total
duration, they are NOT responsible for storing physical information like size, positioning of each rectangular area, fonts, borders, colors,
background images, or any other aesthetic piece of information. We will address this in the next section.

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

For prototyping this system, scheduling is not necessary, as we will pretend that the correct `Presentations` will land on the web page at
the right time.

## Equipment Setup and Execution
As mentioned, an establishment will have one monitor displaying `Presentations` on a web page, and that one monitor that displays that web page is 
actually a `Channel`. This monitor really is hooked up to a physical computer that runs the web page *running on localhost*. This web page will periodically
"poll" for `Presentations` to display, and it "polls" by [attempt] reading one or more .json files. Those .json files are created by the `Presentation`s
presenter console application, also installed and run on the same machine as the localhost web page.

The presenter console application is responsible for obtaining presentations of its host machine's assigned `Channel` that are scheduled to be aired shortly.
More specifically, the console application will query the Frixxer System "give me the next x minutes worth of presentations starting at a specific near-future
time". For each `Presentation` that the console application finds, it downloads all videos as chosen by the user at editing time, figures out exactly
which ads to show (and then download the ads' image files), and gathers all scroll text (which may require specific API calls to multiple 3rd parties), and creates
the "live" version (of a `Presentation`) complete with everything that HTML, CSS, and Javascript needs to execute it. The "live" version ends up in the
.json files that the website looks for.

While the presenter console application is busy polling for content and preparing those .json files, the web page is performing the execution and also looking
for more .json files to present.

## Clientele
This has not been discussed, though we know if someone creates `presentation`s, someone will need to be registered to the system. Tentatively,
we as top-level administrators manually create users. However, the natural inclination is that someone will want to register on behalf of
a company, which allows multiple users of a company to be able to simultaneously create and manage content.

How these clients will pay for our service hasn't been discussed. Payment providers, and even plans, contracts, and allowances have not
been discussed.
