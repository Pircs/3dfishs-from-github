--region FishingGameTableManager.lua
--Author : wangzw
--Date   : 2015/4/7
--此文件由[BabeLua]插件自动生成

local FishingGameTableManager = class("FishingGameTableManager")

local FishingGameTable = require("Tables.FishingGameTable")
local Table_UI = require("Tables.Table_UI")
FishingGameTableManager = {tableMap={}}

function FishingGameTableManager:loadTables()

    self:readTable("table_ui",Table_UI.new())
    
end

function FishingGameTableManager:readTable(tablename, tab)
    local size = 0
    local configSourceStr = ResourceManager:LoadFile("config/tables",tablename)
    local configLines = string.split(configSourceStr,'\n')
    local index = 0
    for k in pairs(configLines) do
        local oneline = configLines[k]
        if index == 0 then
            index = index + 1
        elseif string.sub(oneline,1,1) == "#" then
        elseif oneline ~= nil then
            local values = string.split(oneline,'\t')
            for var in pairs(values) do
                values[var] = string.rtrim(values[var])
                tab:addValue(index,var,values[var])
            end
            index = index + 1
        end

    end
    self.tableMap[tablename] = tab
end

function FishingGameTableManager:getTable(tablename)
    return self.tableMap[tablename]
end

return FishingGameTableManager
--endregion
