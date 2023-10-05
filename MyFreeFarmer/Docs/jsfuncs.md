## Stuff to know

 :o: ``The function's documentation isn't 100% factual.``\
 :heavy_check_mark: ``This function is marked as factual, nothing more to add.``\
 :raised_back_of_hand: ``Function previously marked as done and factual has been re-visited and changed due to notice.``

***

### Login using login-form input (instead of clicking login button)

```javascript
createToken();
```
>* **This function has no parameters.

**_You still need to insert the user name, password and choose between servers "yourself" via html click_**

#### :o: Marked as uncompleted in its documentation

***

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
#### :o: Marked as uncompleted in its documentation

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
### :heavy_check_mark: Function documentation has been marked as done.

***

### Navigating (between farms and map)

```javascript
mapGo2Location((string)location, (int)location_index);
```
>* **Parameters:**
>    * `(string)location`: What "type" of location
>    * `(int)location_index`: Index of the location, e.g Farm 4 is location = farm, and index = 4.

**_This navigates the game to an area, also a sub-area if available!_**\
**_:warning: See [Location Names](https://github.com/michael-fa/FarmBot/tree/master/MyFreeFarmer/location_names.md) for more!._**\
#### :o: Marked as uncompleted in its documentation
:bulb: Using this as a farm is the equivalent of initLocation which can be called from anywhere (needs more checking!)

***

### Planting item on garden spot (garden)

:info: First make sure you opened the position (farm land) first. 

```javascript
parent.cache_me((int)pos, (int)item_id, (int[])prod_idx = garten_prod[item_id], (int[])cat_idx = garten_kategorie[item_id]);
```
>* **Parameters:**
>    * `(int)pos`: The id of the position on the farm
>    * `(int)item_id`: The id of the item to be planted.
>    * `(int)prod_idx`: Simply use garten_prod[item_id] in this place.
>    * `(int)cat_idx`: Simply use garten_kategorie[item_id] in this place.

**_This should plant / cultivate a spot on a acre locted on your farm._**
**_To place on another farm you should first move to it, then use this function._**
#### :o: Marked as uncompleted in its documentation

***

### Display a global box

:info: This displays a global box kind of as a info box, with title, text, check and close icon.

```javascript
globalBox((string)"title", (string)"content", (function)onClickYes(), (function)onClickCancel, (bool)no_cancel);
```
>* **Parameters:**
>    * `(string)title`: The box title.
>    * `(string)content`: The message to display.
>    * `(function)onClickYes`: The function to execute when green icon is clicked.
>    * `(function)onClickCancel`: The function to execute when red icon is clicked.
>    * `(int)show_check`: is onClickCancel is null and this 1, only the green will appear.
>    * `unk`: This parameter has not been identified yet.
>    * `unk`: This parameter has not been identified yet.
**_As for the first paramters it should work fine as explained!_**
#### :o: Marked as uncompleted in its documentation

***

### Select item from rack

:info: To interact with on positions (e.g planting on farm land)
```javascript
globalBox((string)"title", (string)"content", (function)onClickYes(), (function)onClickCancel, (bool)no_cancel);
```
>* **Parameters:**
>    * `(int)itemid`: ID of the item.

**_:warning: See [ITEM IDs](https://github.com/michael-fa/FarmBot/blob/master/MyFreeFarmer/Docs/item_ids.md) for more!._**\
### :heavy_check_mark: Function documentation has been marked as done.