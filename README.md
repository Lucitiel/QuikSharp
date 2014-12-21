﻿QuikSharp
==========
QuikSharp is Quik Lua interface in .NET.

QuikSharp exposes all functions and events available in QLUA as C# async functions
and events.

A simple Ping/Pong benchmark shows c.80 microseconds roundtrip time for a standalone 
Lua interpreter and c.190 microseconds for Quik (MacBook Air 2012). This is almost negligible 
compared to Quik's native latency (from the terminal to a remote server).

Install & Usage
==================

Use NuGet to install QuikSharp into your project. 

    PM> Install-Package QuikSharp


A folder `lua` with all required scritps will be added to your project. 
Start `QuikSharp.lua` script from Quik and normally never stop it
manually (it will be started automatically when Quik starts). See unit tests for 
usage examples in C#.


Why use .NET for Quik
=============
(and not Lua or DDE or other libs)

* Because one should be crazy to use 3rd party closed source code with unclear
licensing and support for trading (even tested and popular like StockSharp).

* Because Quik is dumb, slow, illogical, made by violent lazy
undergrads for users to suffer (no offence, just trolling) - while .NET is smart, 
performant and pleasure to work with.

* Because Quik is a niche legacy soft that has its market share for 
historical reasons. One should not invest more than a minimum into such software, but 
should abstract away its idiosyncrasies as much as possible. Most platforms have .NET API
and it is the choice for new development.


LICENCE
============
QuikSharp - Quik Lua interface in .NET
Copyright Ⓒ 2014 Victor Baybekov

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.

