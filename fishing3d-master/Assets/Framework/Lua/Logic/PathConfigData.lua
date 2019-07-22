--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
local json = require("cjson")

local PathConfigData = class("PathConfigData")

function PathConfigData:ctor()
    self.controlPointsData = {}
end

function PathConfigData:getInstance()
    if self.instance_ == nil then
        self.instance_ = PathConfigData.new()
    end

    return self.instance_
end

function PathConfigData:loadData(args)
    --这一部分不能写死，否则加了路径这里还要改数量
    for i = 0,5  do
        local str = ResourceManager:LoadFile("config/pathes",tostring(i))
        local onePath = json.decode(str)
        self.controlPointsData[i] = onePath
    end
end

function PathConfigData:getControlData()
    return self.controlPointsData[math.random(0,5)]
end

return PathConfigData
--endregion
