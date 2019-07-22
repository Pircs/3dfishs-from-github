
local CoinAnimationView = require("View.CoinAnimationView")
local GameTableManager = require("Tables.FishingGameTableManager")
local GameUIManager = class("GameUIManager")

function GameUIManager:ctor()
    --ui Root
    self.uiRoot_ = GameObject.FindWithTag("UIRoot")
end

function GameUIManager:getInstance()
    if self.instance_ == nil then
        self.instance_ = GameUIManager.new()
    end
    return self.instance_
end

function GameUIManager:getUIRootTransform()
    return self:getInstance().uiRoot_.transform
end

function GameUIManager:getUIRootObject()
    return self:getInstance().uiRoot_
end

function GameUIManager:viewObjCreatedCallback(obj)
    self.activeViewMap[obj.name] = obj
end

function GameUIManager:hideView(viewname)
    for key,value in pairs(self.activeViewMap) do
        if key == viewname then
            value:Destroy()
            self.activeViewMap[viewname] = nil
        end
    end
end

function GameUIManager:hideAll()
    for key,value in pairs(self.activeViewMap) do
        value:Destroy()
        self.activeViewMap[key] = nil
    end
end

function GameUIManager:getView(viewName)
    
end

function GameUIManager:showCoinAnimationView()
    self.coinAnimationView_ = CoinAnimationView.new()
end

function GameUIManager:getCoinAnimationView()
    return self.coinAnimationView_
end

function GameUIManager:showIslandSelectView()
    local viewName = "IslandSelectView"
    local view = self.uiRoot_.transform:FindChild(viewName)
    if view then
        return
    end
    self.islandSelectView_ = require("View.IslandSelectView").new(self)
    self.islandSelectView_:setName(viewName)
    self.islandSelectView_:setLayer("UI")
    self.islandSelectView_:setParent(self.uiRoot_.transform)
    self.islandSelectView_:setIdentity()
end

function GameUIManager:hideIslandSelectView()
    if self.islandSelectView_ then
        self.islandSelectView_:destroy()
        self.islandSelectView_ = nil
    end
end

function GameUIManager:showMainView()
    local viewName = "MainView"
    local view = self.uiRoot_.transform:FindChild(viewName)
    if view then
        return
    end
    self.mainView_ = require("View.MainView").new(self)
    self.mainView_:setName(viewName)
    self.mainView_:setLayer("UI")
    self.mainView_:setParent(self.uiRoot_.transform)
    self.mainView_:setIdentity()
end

function GameUIManager:getMainView()
    return self.mainView_
end

function GameUIManager:showBulletLayer()
    local layerName = "Layer_Bullet"
    local layerObj = self.uiRoot_.transform:FindChild(layerName)
    if layerObj then
        return
    end
    self.bulletLayer_ = require("View.BulletLayer").new(self)
    self.bulletLayer_:setName(layerName)
    self.bulletLayer_:setLayer("UI")
    self.bulletLayer_:setParent(self.uiRoot_.transform)
    self.bulletLayer_:setIdentity()
end

function GameUIManager:getBulletLayer()
    return self.bulletLayer_
end

function GameUIManager:showBombLayer()
    local layerName = "Layer_Bomb"
    local layerObj = self.uiRoot_.transform:FindChild(layerName)
    if layerObj then
        return
    end
    self.bombLayer_ = require("View.BombLayer").new(self)
    self.bombLayer_:setName(layerName)
    self.bombLayer_:setLayer("UI")
    self.bombLayer_:setParent(self.uiRoot_.transform)
    self.bombLayer_:setIdentity()
end

function GameUIManager:getBombLayer()
    return self.bombLayer_
end

function GameUIManager:initializeTouchLayer()
    local layerName = "Layer_TouchEvent"
    local layerObj = self.uiRoot_.transform:FindChild(layerName)
    if layerObj then
        return
    end
    self.touchEventLayer_ = require("View.TouchEventLayer").new(self)
    self.touchEventLayer_:setName(layerName)
    self.touchEventLayer_:setLayer("UI")
    self.touchEventLayer_:setParent(self.uiRoot_.transform)
    self.touchEventLayer_:setIdentity()
end

function GameUIManager:initializeFishLayer()
    local layerName = "Layer_Fish"
    self.fishLayer_ = require("View.FishLayer").new(self)
    self.fishLayer_:setName(layerName)
    self.fishLayer_:setLayer("Fish")
    self.fishLayer_:setIdentity()
end

function GameUIManager:getFishLayer()
    return self.fishLayer_
end

return GameUIManager
