--region TouchEventLayer.lua
--Date
--此文件由[BabeLua]插件自动生成
local TouchEventLayer = class("TouchEventLayer",require("View.NodeBase"))

function TouchEventLayer:ctor(uimanager)
    TouchEventLayer.super.ctor(self)
    self.pressedTime_ = 1
    self.pressed_ = false
    self.uiManager_ = uimanager
    self:initView()
    self:initEventCallback()
end

function TouchEventLayer:initView()
    self:setObject("ui","View_TouchEventLayer")

    local proxy = self.gameObject_:AddComponent(typeof(ScriptProxy))
    proxy.Table = self
end

function TouchEventLayer:initEventCallback()
    local eventTrigger = self:getChild("TouchLayer"):GetComponent("JJEventTrigger")
    if eventTrigger then
        eventTrigger:AddPressCallback(handler(self,self.onPressed),nil)
        eventTrigger:AddReleaseCallback(handler(self,self.onReleased),nil)
    end
end

function TouchEventLayer:onPressed(args)
    self.pressed_ = true
end

function TouchEventLayer:onReleased(args)
    self.pressed_ = false
    self.pressedTime_ = 1
end

function TouchEventLayer:onUpdate(dt)
    if not self.pressed_ then
        return
    end
    self.pressedTime_ = self.pressedTime_ + dt
    if self.pressedTime_ > 0.3 then
        self.pressedTime_ = 0
        local touchPos = MathUtil.ScreenPos_to_NGUIPos(UnityEngine.Input.mousePosition);
        local from = touchPos - Vector3.New(0, -320,0);
        if from.y < 0 then
            from.y = 0
        end
        local angle = Vector3.Angle(from, Vector3.right);
        self.uiManager_:getMainView():playCannonAnimation(0,angle)
        self.uiManager_:getBulletLayer():createBullet(0,from)
    end
end

return TouchEventLayer
--endregion
