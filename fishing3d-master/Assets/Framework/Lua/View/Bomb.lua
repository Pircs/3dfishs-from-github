--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
local Bomb = class("Bomb",require("View.NodeBase"))

function Bomb:ctor(bomblayer)
    Bomb.super.ctor(self)
    self.active_ = false
    self.bombLayer_ = bomblayer
    self:initView()
end

function Bomb:initView()
    self:setObject("bomb","Bomb_01")
end

function Bomb:bomp()
    local animation = self:getChild("Animation"):GetComponent("UISpriteAnimation")
    animation:Play()
    animation.animationCallback = handler(self,self.bombCallback)
end

function Bomb:bombCallback()
    self.bombLayer_:recycleBomb(self)
end

return Bomb
--endregion
