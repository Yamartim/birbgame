# birbgame

A small 3D platformer for an internship assignment.

You are a small bird and you gotta climb the mountain to reach your nest! Explore to find coins to make the climb easier!

> Design and implement a Unity app containing a single mini game that distracts the user during a painful procedure, with a focus towards children.

You can play it on Windows by cloning the repository and executing the file `Builds/0.1.4/birbgame.exe` however I'd recommend playing straight from the unity editor (version 2020.3.18f1) as there are some very weird bugs on the build version that make it hard to play... (a mouse and keyboard or a gamepad are required to play)

## Design Process

First off I tried to imagine being in the situation described in the assignment: *"if when I was a kid I had to do some procedure what sort of game would i want to play?"* from that I listed some of the design pillars of the game:

 - the game to be as easy as possible to understand so anyone can play it
 - the game needs to be interesting and engaging as possible to be a distract the player from what they're going through
 - the game needs to be playable with as few buttons as possible as certain medical procedures could limit the use of one's hands

With that settled I went looking for games to get inspired by and make something new!

My main inspiration was by far the indie game [A Short Hike](https://ashorthike.com/) and it's pretty easy to tell! The game hits exactly the mood I would want for my game in this assignment: It's very simple, relaxing and incites your curiosity and sense of wonder so you can't stop playing it until you finish!

The second inspiration was Nintendo's Zelda Breath of the wild, of course even though the scope of that can't even be compared to this probject it can still teach us quite a few lessons in what makes a game engaging such as how much freedom for the player it provides and how it presents challenges that can be solved creatively in many different ways

And Last but not least the final inspiration was also from nintendo: Super Mario 64 a classic that is still widely enjoyed to this day. What I took from this game was the importance of having such fun movement mechanics that just the act of playing the game is fun by itself and allows people to make their own fun in the game and not necessarily follow the path set by the developer if they don't feel like it.

After that and some more consideration I was able to narrow down what sort of game would be ideal for this assignment:

 - The game will be a 3D plarformer with a focus on exploration, this genre is loved for being generally both very accessible but also offering a lot of enjoyment and is also decently niche nowdays so it'll stand out as not many people make them.
 - the game will only have two inputs: one movement input and a jump input so that it'll be easy to understand and won't leave people who don't play games often overwhelmed
 - the game will need a very tangible sense of progression to keep players engaged and interesting
 - the game will have a main objective but there's no harm in players ignoring it and doing what they find fun 
 - the game will reward exploration, curiosity and creativity.

From this point it all came very fluidly and just fit pretty well with my original plans.

## Game architecture

(pretty class diagram of the project goes here when it's ready)

### Main objects

#### Player character

##### Movement

##### Wing System

#### Interactables

##### Coins

##### The Nest

##### Bottomless Pits

#### UI

##### Time Trial Feature

##### CoinBar Display

### Unity features and design patterns


## Conclusion

This was an extremely fun assignment to take part in! It felt similar to a gamejam which is a great feeling.

There were some features and polish I wanted to include but alas that was all time permitted, my biggest regrets by far were bit being able to include sound and particle effects, that would've improved the gamefeel considerably,

On the 0ther hand I used a lot of things I never did before and that felt great! For the first time I made a fully 3D game in Unity which I had never done before while using many resources I was unfamiliar with such as Cinemachine and Shadergraph, but now I'm quite confident that I can make use of all of these things and that I have improved as a developer. 