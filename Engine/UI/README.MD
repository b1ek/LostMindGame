## Documentation
This document contains all information about everything you can see here and how to use it properly.
---
# UIElement.cs
Element developing in 3 lines:<br/>
 - put the element text in the `innerText` field
 - if you demand custom printing method, simply overload `print(...)`
 - to process click, overload `click()`
Element API documentation is in elApi.MD.
# Viewport.cs
Base class for displaying elements.<br/>
```cs
Viewport view = new Viewport(0, 0, Console.BufferWidth, Console.BufferHeight);
view.DumpArea(); // to clean the stuff you have in the area
view.AddElement(new UILabel("hi!", ConsoleColor.Cyan));
view.AddElement(new UILabel("This element will be removed."));
view.AddElement(new UIButton("Quit viewport", () => {view.Dispose();}));
view.RemoveElement("Quit viewport");
view.Mainloop(); // tkinter-like
// you can use MainloopAsync if you want
```
Most of the methods/properties are not documented at this moment, but they will be.
# UIButton.cs
As the name says, the button. Use it like this:
```cs
UIButton button = new UIButton("Click me!");
button.OnClick += YourHandler; // 0 args
yourViewport.AddElement(button);

// or if you are too lazy to do all the variable stuff
yourViewport.AddElement(new UIButton("Click me!", yourHandler));
```
# UILabel.cs
Holds only one string. Actually, you can use bare UIElement,<br/>
but using label is way easier:```cs
UILabel txt = new UILabel("Some text", ConsoleColor.IfYouWant);
txt.Color = ConsoleColor.SomeNewColor; // dont forget to re-print the element
```
# UISysConfig.cs
Class with keys config<br/>
(it will be removed _soon_)
# UITextInput.cs
Used for getting user text input. <br/>
Pretty much raw stuff. Needs more development.