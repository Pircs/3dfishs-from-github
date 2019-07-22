--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
local json = require("cjson")
local EventManager = class("EventManager")
local UIManager = require("Logic.GameUIManager")

function EventManager:ctor()
    self.eventList_ = {}
    self.timer_ = 0
end

function EventManager:getInstance()
    if self.instance_ == nil then
        self.instance_ = EventManager.new()
        math.randomseed(os.time())
    end

    return self.instance_;
end

function EventManager:loadEventConfig()
    local str = ResourceManager:LoadFile("config/tables","event")
    self.eventList_ = json.decode(str)
end

function EventManager:caculateBeginAndEndTime()
    if self.eventList_ ~= null then
        local t = 0
        for i,value in ipairs(self.eventList_) do
            value.beginTime = t
            value.endTime = t + value.t
            t = value.endTime
        end
    end
end

function EventManager:onUpdate(dt)
    self.timer_ = self.timer_ + dt
    if self.timer_ > 0.4 then
        self.timer_ = 0
        self:testOneFish()
    end
end

function EventManager:testOneFish()
    local hBound = 150;
    local yBottom = -70;
    local yUp = 70;
    local temp = { -1, 1 };
    local flag = temp[math.random(1, 2)];
    local speed = math.random(50, 80);
    local pathid = math.random(0, 5);
    local temp1 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    local fishid = temp1[math.random(1, 10)];
    if fishid == 1 then
        speed = 30
    end
    if fishid == 2 then
        speed = 20
        pathid = 0
    end

    local headPosition = Vector3.New(hBound * flag, math.random(yBottom + 20, yUp - 20), math.random(150, 150 + 20));
    local bornEulerAngles = Vector3.New( math.random(-20, 20), - 90 * flag, 0);
    UIManager:getInstance():getFishLayer():createFish(fishid, headPosition, bornEulerAngles, pathid, speed, 0);
end

return EventManager
--endregion
