--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
local Cannon = class("Cannon",require("View.NodeBase"))

function Cannon:ctor()
    Cannon.super.ctor(self)
    self:initView()
end

function Cannon:initView()
    self:setObject("cannon","Cannon_01")
    if self.gameObject_ == nil then
        return
    end
end

return Cannon
--endregion