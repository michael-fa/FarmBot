# FarmBot :warning:  WIP
A bot for my free farm, aiming to do stuff automatic.

## :tv: [Streaming on YouTube!](https://www.youtube.com/watch?v=l3HopvETf3s)

❗ Remember that doing bot stuff on your account will lead to an instant ban, once official game moderators have noticed.
This is a research and progression thing for me personally but it'll be released soon.

## Planned: 

Gameplay 'functions', which normally are triggered by a player clicking or interacting with the game inside the browser, are simulated using a [Webdriver] and on top of that, all those actions can be scripted using the [Pawn] scripting language.
For harvesting a single field to watering / planting all at once - everything has it's function. 
So imagine you have to loop 'x' through 1 to 120 and just call HarvestGarden(x) and done. 
But there are functions like HarvestAll, WaterAll and so on..

### Notes
 - I haven't found any limits calling the scripting functions in like a loop without wait/sleep
   - The reason is that most of the time I try to run the JS functons from the game itself, using INSPECT with the browser (easy af)
   
   1. One of the six spots on each farm is called a (Farm)position.
   2. A farm, any market, picknic place and all are called Locations.
   3. Factory stands for bakeries, cheese-factory and other (food-) processing
   4. Building or animal means any animal housing.
  
   #### State of project
   - Given my available free time, it is already possible to compile scripts. I won't be putting out a final executable.
   - I am willing to give away "private versions" though. (Meaning also that I won't be publishing ALL code, leaving out future planned stuff.
   

### Gardens/Standard fields
 - Auto harvesting
 - Auto planting
 - Auto watering
 - Auto Boost
 
### 🐑🐏🐮🐝🐟 Farms
 - Auto retrieve finished product
 - Auto feed (after template/preset)


[Webdriver]: https://www.google.com/search?q=what+is+a+webdriver&client=firefox-b-d&sca_esv=8c23647d28d0699c&sxsrf=AE3TifPDQvrIhqpCW9K2lEU8SvG5nY2nVA%3A1759443364978&ei=pPneaPy1O4aU9u8PtNWk6AM&ved=0ahUKEwj8l5amxYaQAxUGiv0HHbQqCT0Q4dUDCBI&uact=5&oq=what+is+a+webdriver&gs_lp=Egxnd3Mtd2l6LXNlcnAiE3doYXQgaXMgYSB3ZWJkcml2ZXIyCBAAGIAEGMsBMggQABiABBjLATIGEAAYFhgeMgYQABgWGB4yBhAAGBYYHjIGEAAYFhgeMggQABgWGAoYHjIIEAAYFhgKGB4yBRAAGO8FSN8XULwGWK4XcAF4AZABAJgBT6AB8AmqAQIxOLgBA8gBAPgBAZgCE6AC6wrCAgoQABiwAxjWBBhHwgINEAAYgAQYsAMYQxiKBcICBBAjGCfCAgoQIxiABBgnGIoFwgIKECMY8AUYJxjJAsICChAAGIAEGEMYigXCAgsQABiABBixAxiDAcICEBAAGIAEGLEDGEMYgwEYigXCAgUQABiABMICEBAjGPAFGIAEGCcYyQIYigXCAggQABiABBixA8ICDhAuGIAEGLEDGNEDGMcBwgILEC4YgAQYsQMY1ALCAgQQABgDwgIFEC4YgATCAgoQABiABBgKGMsBmAMAiAYBkAYKkgcCMTmgB7V2sgcCMTi4B-UKwgcIMC4yLjE1LjLIB1o&sclient=gws-wiz-serp

[Pawn]: https://www.compuphase.com/pawn/pawn.htm
