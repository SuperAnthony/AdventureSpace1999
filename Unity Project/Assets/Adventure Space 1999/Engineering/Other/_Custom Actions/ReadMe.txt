/********************/
/** Custom Actions **/
/********************/

Custom actions use the WhatPumpkin abstract type, Action (not to be confused with System.Actions).

Actions have a name and parameters associated with them. Developers can create custom actions to do just about anything anytime an action is called, and receive any number of paramters to handle each action accordingly. 

The entry point of an action when it is used is BeginAction. The begin action method is invoked and receives members. BeginAction is IEnumerable so that it can be used as a coroutine. 

These are meant to allow designers to use actions with simple parameters to execute commands with the engineers doing the heavy lifting (taking away the need for a designer to learn a scripting language).

Actions wait to be completed before moving on to the next action. Most custom actions are "completed" instantaneously action sequences to move on to the next action automatically.

Therefore, EndAction() must always be invoked when the action has completed.

Examples of custom actions are:

Example 1:

Action: SetNar
Parameters: "C is for cookie, that's good enough for me."

In this example I use SetNar to let the game know I'm going to display narrator text and the string paramters gets unboxed and is what gets displayed.

Example 2:

Action: SetVar
Parameters: numCookies, 12

This sets the LUA variable numCookies to 12

Example 3:

Action: Play
Parameters: MINI_GAME_SIMON

This looks for an IKeyed object called MINI_GAME_SIMON, checks to see it implements IPerformance , if so, it invokes the mini game's play method and waits for the user to complete the mini game before ending the action and moving on to the next one.

Example 4:

Action: Play
Paramters: SFX_COOKIE_MUCH

Like the mini game example, this looks for an IPerformance object of the same name and attempts to play it. In this example it would wait for a sound to complete playing before moving on to the next action sequence.

Example 5:

Action: Start
Paramters: SFX_COOKIE_MUCH

Start was a custom action created to allow an Action Sequence to move from one action to the other instantaneously. Start, was an action I created that "Ends" immediately. So that, in this example, that sound would start playing, the action would end before the sound completes, but the next action could still be played.