--region Table_UI.lua
--Date 2016.6.7
--此文件由[BabeLua]插件自动生成
local Table_UI = class("Table_UI",require("Tables.FishingGameTable"))

local Table_UI_Field =
{
    ID = 1,
    ViewName = 2,
    PrefabName = 3,
    ScriptName = 4
}

function Table_UI:addValue(index,column,values)
    local record = nil
    if #(self.records) < index then
        record = {}
        self:addRecord(record)
    else
        record = self.records[index]
    end
    if column == Table_UI_Field.ID then
        record.id = toint(values)
    elseif column == Table_UI_Field.ViewName then
        record.viewName = values
    elseif column == Table_UI_Field.PrefabName then
        record.prefabName = values
    elseif column == Table_UI_Field.ScriptName then
        record.scriptName = values
    end
end

function Table_UI:getRecordByName(viewName)
    for key,value in pairs(self.records) do
        if viewName == value.viewName then
            return value
        end
    end
    return nil;
end

return Table_UI
--endregion
