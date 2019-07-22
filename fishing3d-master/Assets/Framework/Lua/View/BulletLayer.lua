--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
local BulletLayer = class("BulletLayer",require("View.NodeBase"))
function BulletLayer:ctor(uimanager)
    self.bulletsPool_ = {}
    self.uiManager_ = uimanager
    self:initView()
    self:initBulletsPool()
end

function BulletLayer:initView()
    self.gameObject_ = GameObject.New()
    local panel = self.gameObject_:AddComponent(typeof(UIPanel))
    panel.depth = 1
end

function BulletLayer:initBulletsPool()
    local Bullet = require("View.Bullet")
    for i=0,49 do
        local bullet = Bullet.new(self)
        bullet:setParent(self.gameObject_.transform)
        bullet:setActive(false)
        bullet:setIdentity();
        bullet:setPosition(-10000,-10000,0)
        bullet.active_ = false
        self.bulletsPool_[i] = bullet
    end
    
end

function BulletLayer:createBullet(seatid,dir)
    local bullet = self:getBulletFromPool()
    local cannon = self.uiManager_:getMainView():getCannon(seatid)
    if bullet and cannon then
        local normaldir = dir.normalized
        local angle = Vector3.Angle(dir, Vector3.right);
        bullet:setEulerAngles(0,0,angle)
        bullet:setPosition(cannon:getPosition() + normaldir * 50)
        local p = bullet:getPosition()
        --print(p.x,p.y,p.z,normaldir.x,normaldir.y,normaldir.z)
        
        bullet:setScale(Vector3.one)
        bullet.dir_ = normaldir
    end
end

function BulletLayer:getBulletFromPool(a)
    for i,value in pairs(self.bulletsPool_) do
        if value.active_ == false then
            value.active_ = true
            value:setActive(true)
            return value
        end
    end
    return nil
end

function BulletLayer:recycleBullet(bullet)
    if bullet == nil then
        return
    end
    bullet.active_ = false
    bullet:setActive(false)
    bullet:setPosition(-1000,-1000,0)
end

return BulletLayer
--endregion
