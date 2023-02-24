### Show farm pos name (farm_pos_tt_name)

```javascript
farmHoverPosition( (bool)toggle, (int)farm_id, (int)pos_id);
```
>* **Parameters:**
>    * `(bool)toggle`: 1 show, 0 hide.
>    * `(int)farm_id`: What farm is that position on.
>    * `(int)pos_id`: The position from which a name should be shown'

**_Shows the `farm_pos_tt_name` div. These don't show the actual buttons! To show buttons you should use:_**
```javascript
showDiv('farm_buttons1_1'); 
```


***

### Starting a factory's production

```javascript
factory.start("start", (int)farm_pos, (int)product_type, (int)production_slot);
```
>* **Parameters:**
>    * `(int)farm_pos`: the actual position of the factory/building on the farm.
>    * `(int)product_type`: Which one of the products should the factory start produce
>    * `(int)production_slot`: and for what slot

**_Starts the production for a product on the given slot if available_**

***

### Opening a farm pos

```javascript
specialZoneFieldHandler((int)farm_pos);
```
>* **Parameters:**
>    * `(int)farm_pos`: the actual position of the garden.

**_This opens the position of a  farm._**\
**_:warning: Note that this is only for acre land (garden)! For other position types (factories and buildings) try the following:_**

```javascript
initLocation((int)farm_pos);
```

### Navigating (between farms and map)

```javascript
mapGo2Location((string)location, (int)location_index);
```
>* **Parameters:**
>    * `(string)location`: What "type" of location
>    * `(int)`: Index of the location, e.g Farm 4 is location = farm, and index = 4.

**_This navigates the game to an area, also a sub-area if available!_**\
**_:warning: See [Location Names](https://github.com/michael-fa/FarmBot/tree/master/MyFreeFarmer/location_names.md) for more!._**\
#### :o:
:bulb: Using this as a farm is the equivalent of initLocation which can be called from anywhere (needs more checking!)



