Feature: Squeezebox Player Controls
	In order to enjoy my music
	As a Logitech Squeezebox client
	I want to be control playback of my music
	
@integration
Scenario Outline: Press buttons
	Given a Squeezebox player <playerId> managed by host <host> on port <port>
	Given that the player is powered off
	When I press the power button
	Then the player is powered on
	Given a queued playlist
	When I press the play button
	Then the player mode is playing
	When I press the pause button
	Then the player mode is paused
	When I press the play button
	And I press the next button
	Then the current track changes
	When I press the shuffle button
	Then the shuffle mode changes
	When I press the shuffle button
	Then the shuffle mode changes
	When I press the shuffle button
	Then the shuffle mode changes
	When I press the repeat button
	Then the repeat mode changes
	When I press the repeat button
	Then the repeat mode changes
	When I press the repeat button
	Then the repeat mode changes
	Examples:
	|host|port|playerId|
	|192.168.1.103|9000|00:04:20:17:8e:e6|

@integration
Scenario Outline: Set volume
	Given a Squeezebox player <playerId> managed by host <host> on port <port>
	Given that the player is powered on
	When I set the volume to 50
	Then the volume is 50
	When I set the volume to -10
	Then the volume is 0
	When I set the volume to 105
	Then the volume is 100
	Examples:
	|host|port|playerId|
	|192.168.1.103|9000|00:04:20:17:8e:e6|

@integration
Scenario Outline: Set shuffle mode
	Given a Squeezebox player <playerId> managed by host <host> on port <port>
	Given that the player is powered on
	When I set the shuffle mode to all
	Then the shuffle mode is all
	When I set the shuffle mode to album
	Then the shuffle mode is album
	When I set the shuffle mode to off
	Then the shuffle mode is off
	Examples:
	|host|port|playerId|
	|192.168.1.103|9000|00:04:20:17:8e:e6|

@integration
Scenario Outline: Set repeat mode
	Given a Squeezebox player <playerId> managed by host <host> on port <port>
	Given that the player is powered on
	When I set the repeat mode to track
	Then the repeat mode is track
	When I set the repeat mode to playlist
	Then the repeat mode is playlist
	When I set the repeat mode to off
	Then the repeat mode is off
	Examples:
	|host|port|playerId|
	|192.168.1.103|9000|00:04:20:17:8e:e6|
