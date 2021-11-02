```
            __                              __            __       __  __                  __ 
           /  |                            /  |          /  \     /  |/  |                /  |
           $$ |        ______    _______  _$$ |_         $$  \   /$$ |$$/  _______    ____$$ |
           $$ |       /      \  /       |/ $$   |        $$$  \ /$$$ |/  |/       \  /    $$ |
           $$ |      /$$$$$$  |/$$$$$$$/ $$$$$$/         $$$$  /$$$$ |$$ |$$$$$$$  |/$$$$$$$ |
           $$ |      $$ |  $$ |$$      \   $$ | __       $$ $$ $$/$$ |$$ |$$ |  $$ |$$ |  $$ |
           $$ |_____ $$ \__$$ | $$$$$$  |  $$ |/  |      $$ |$$$/ $$ |$$ |$$ |  $$ |$$ \__$$ |
           $$       |$$    $$/ /     $$/   $$  $$/       $$ | $/  $$ |$$ |$$ |  $$ |$$    $$ |
           $$$$$$$$/  $$$$$$/  $$$$$$$/     $$$$/        $$/      $$/ $$/ $$/   $$/  $$$$$$$/

                                     Forged in the depths of hell
                                                by blek
```
## LostMind
My first game. Building an engine for it from scratch
using C# and .NET 5.
Code has not too much documentation for now, but it will be in future.

If someone wants to join me, feel free to create pull requests.

The game engine library
 - Animation ✔
 - GUI ✔
 - Game ✕
 - Documentation - about 50%
 
### Animation
---
A frame-based animation.
You can build a frame sequence, and put it into an animation controller

Or you can split an ascii art using Animation.createSimple("YOUR_ART_STRING", startX, startY, delayBetweenFrames)

#### Example
```cs
var anim = Animation.createSimple("artString", 0, 0, 15);
anim.run();
```

### GUI
---
System is very simple.
You create a viewport, place it in specific coordinates
And put elements in it!
Constructor: Viewport(x, y, width, height)


#### Example
```cs
UserKeyInput.installHook(); // THIS IS IMPORTANT. CALL THIS METHOD IN THE FIRST LINES OF YOUR START POINT
var view = new Viewport(0, 0, 32, 8);
view.addElement(new UIButton("Exit", onClickHandler()));
```

### UserConsoleWriter
---
Also i'd like to tel you about very usable class
which actually does nothing but very useful
when you make a console GUI.

It creates a rectangle area in console
where you can put text into.
Note: The rectangle has no width-height limits.
Means that text can be printed way outside the console window.
Be careful with that.

Constructor: UserConsoleWriter(x, y)

#### Example
```cs
var writer = new UserConsoleWriter(2, 2)
writer.write("This is the writer output!\n") // dont forget to add \n
```
