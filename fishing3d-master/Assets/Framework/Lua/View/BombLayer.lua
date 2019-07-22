--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
local BombLayer = class("BombLayer",require("View.NodeBase"))

function BombLayer:ctor()
    self.bombPool_ = {}
    self:initView()
    self:initBombPool()
end

function BombLayer:initView()
    self.gameObject_ = GameObject.New()
    local panel = self.gameObject_:AddComponent(typeof(UIPanel))
    panel.depth = 1
end

function BombLayer:initBombPool(args)
    local Bomb = require("View.Bomb")
    for i=0,49 do
        local bomb = Bomb.new(self)
        bomb:setParent(self.gameObject_.transform)
        bomb:setPosition(-10000,-10000,0)
        bomb:setActive(false)
        bomb.active_ = false
        self.bombPool_[i] = bomb
    end
    
end

function BombLayer:getBombFromPool()
    for i,value in pairs(self.bombPool_) do
        if value.active_ == false then
            value.active_ = true
            value.gameObject_:SetActive(true)
            return value
        end
    end
    return nil
end

function BombLayer:recycleBomb(bomb)
    if bomb == nil then
        return
    end
    bomb.active_ = false
    bomb:setActive(false)
    bomb:setPosition(-1000,-1000,0)
end

function BombLayer:showBomb(position)
    local bomb = self:getBombFromPool()
    if bomb then
        bomb:setPosition(position)
        bomb:bomp()
    end
end

return BombLayer
--endregion
