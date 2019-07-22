--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
local PathConfigData = require("Logic.PathConfigData")

local FishLayer = class("FishLayer",require("View.NodeBase"))

function FishLayer:ctor()
    self.gameObject_ = GameObject.New()
    self:setIdentity()
end

function FishLayer:createFish(kindid,position,eulerangle,pathid,speed,unactivetime)
    local fish = require("Logic.Fish").new()
    fish:setLayer("Fish")
    fish:setParent(self.gameObject_.transform)
    fish:setPosition(position)
    fish:setScale(Vector3.New(6,6,6))
    fish:setEulerAngles(eulerangle)
    fish:setPathData(PathConfigData:getInstance():getControlData())
    fish:setSpeed(speed)
end

return FishLayer
--endregion
