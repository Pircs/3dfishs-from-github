local Bullet = class("Bullet",require("View.NodeBase"))

function Bullet:ctor(bulletlayer)
    Bullet.super.ctor(self)
    self.active_ = false
    self.dir_ = Vector3.right
    self.speed_ = 700
    self.bulletLayer_ = bulletlayer
    self:initView()
end

function Bullet:initView()
    self:setObject("bullet","Bullet")
    local proxy = self.gameObject_:AddComponent(typeof(ScriptProxy))
    proxy.Table = self
end

function Bullet:setSpeed(speed)
    self.speed_ = speed
end

function Bullet:onUpdate(dt)
    if self.active_ == false then
        return
    end
    local position = self:getPosition() + self.dir_ * dt * self.speed_
    self:setPosition(position)
    position = self:getPosition()
    if position.x < -600 or position.x > 600 or position.y < -360 or position.y > 300 then
        self.bulletLayer_:recycleBullet(self)
        self.bulletLayer_.uiManager_:getBombLayer():showBomb(position)
        return;
    end

    local worldpos1 = UICamera.currentCamera.transform:TransformPoint(position)
    local pos = UICamera.currentCamera:WorldToScreenPoint(worldpos1)
    local ray = Camera.main:ScreenPointToRay(pos)
    local layerMask = 2 ^ LayerMask.NameToLayer("Fish")
    local flag,hitInfo = Physics.Raycast(ray, nil, 10000, layerMask)
    if flag then
        self.bulletLayer_.uiManager_:getBombLayer():showBomb(position)
        local coinAnimationView = require("Logic.GameUIManager"):getInstance():getCoinAnimationView()
        coinAnimationView:popCoin(position)
        self.bulletLayer_:recycleBullet(self)
    end
end

return Bullet