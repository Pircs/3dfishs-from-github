--主入口函数。从这里开始lua逻辑
require "Common.define"
local Game = require "Logic.Game"
function Main()					
	 Game:OnInitOK()
end

--场景切换通知
function OnLevelWasLoaded(level)
	Time.timeSinceLevelLoad = 0
end