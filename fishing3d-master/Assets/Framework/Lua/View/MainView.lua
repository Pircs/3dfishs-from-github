--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
local EventManager = require("Logic.EventManager")
local PathConfigData = require("Logic.PathConfigData")
local Cannon = require("View.Cannon")
local MainView = class("MainView",require("View.NodeBase"))

function MainView:ctor(uimanager)
    MainView.super.ctor(self)
    self.uiManager_ = uimanager
    self.cannons_ = {}
    self:initView()
end

function MainView:initView()
    self:setObject("ui","View_Main")
    if self.gameObject_ == nil then
        return
    end
    self.cannonRoot_ = self:getChild("CannonsRoot")
    local backBtn = self:getChild("Btn_Back"):GetComponent("JJButton")
    if backBtn then
        backBtn:AddClickCallback(handler(self,self.onClickBackBtn),nil)
    end

    self:createCannon(0)

    EventManager:getInstance():loadEventConfig()
    PathConfigData:getInstance():loadData()

    self:showSeaBg()
    self.uiManager_:showCoinAnimationView()
    self.uiManager_:showBulletLayer()
    self.uiManager_:showBombLayer()
    self.uiManager_:initializeTouchLayer()
    self.uiManager_:initializeFishLayer()

    local proxy = self.gameObject_:AddComponent(typeof(ScriptProxy))
    proxy.Table = self
end

function MainView:onClickBackBtn(args)
    self.uiManager_:hideAll()
    self.uiManager_:showView("IslandSelect")
end

function MainView:createCannon(seatid)
    if seatid == 0 then
        local cannon = Cannon.new()
        cannon:setName("Cannon_" .. tostring(seatid))
        cannon:setParent(self.cannonRoot_)
        cannon:setIdentity()
        cannon:setPosition(0,-320,0)
        cannon:setEulerAngles(0,0,90)
        self.cannons_[0] = cannon
    elseif seatid == 1 then
    elseif seatid == 2 then
    elseif seatid == 3 then
    end
end

function MainView:playCannonAnimation(seatid,angle)
    local animation = self.cannons_[seatid]:getChild("Animation"):GetComponent("UISpriteAnimation")
    animation:ResetToBeginning()
    animation:Play()
    self.cannons_[seatid]:setEulerAngles(0, 0, angle);
end

function MainView:onUpdate(dt)
    EventManager:getInstance():onUpdate(dt)
end

function MainView:showSeaBg()
    self.seabgObj_ = ResourceManager:CreateObject("simplePrefabs","SeaBG")
    self.seabgObj_.transform.localPosition = Vector3.New(0,0,300)
    self.seabgObj_.transform.localScale = Vector3.New(64,1,36)
    self.seabgObj_.transform.eulerAngles = Vector3.New(90,180,0)
    self.seabgObj_.gameObject.name = "SeaBG"
end

function MainView:getCannon(seatid)
    return self.cannons_[seatid]
end

return MainView
--endregion
