require "Common/functionsQuick"
require "Common/functions"
require "View/IslandSelectView"

--管理器--
local Game = {}

local GameTableManager = require("Tables.FishingGameTableManager")
local GameUIManager = require("Logic.GameUIManager")

function Game:initConfigs()
    GameTableManager:loadTables()
end

--初始化完成，发送链接服务器信息--
function Game:OnInitOK()
    self:initConfigs()
    GameUIManager:getInstance():showIslandSelectView()
end

--销毁--
function Game.OnDestroy()
	--logWarn('OnDestroy--->>>');
end

return Game