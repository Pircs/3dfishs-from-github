local Fish = class("Fish",require("View.NodeBase"))
local SECOND_ONE_FRAME = 0.02;
function Fish:ctor(obj)
    Fish.super.ctor(self,obj)
    self.pathData_ = nil
    self.currentLife_ = 0
    self.lastFrameLife_ = 0
    self.stepTime_ = 0
    self.currentStep_ = 1
    self.lastFrameStep_ = 1
    self.speedScaleFactor_ = 1
    self.kindId_ = 0
    self.unActiveTime_ = 0
    self.speed_ = 100
    self.oneFinePoint_ = nil

    self:initView()
end

function Fish:initView()
    self:setObject("fish0","Fish_0")
    self.transform_ = self.gameObject_.transform
    local proxy = self.gameObject_:AddComponent(typeof(ScriptProxy))
    proxy.Table = self
end

function Fish:setStatus(status)

end

function Fish:setSpeed(speed)
    self.speed_ = speed
    self.speedScaleFactor_ = self.speed_ / self.pathData_.baseSpeed
end

function Fish:setPathData(path)
    self.pathData_ = path
end

function Fish:onUpdate(dt)
    --self.transform_.localPosition = self.transform_.localPosition + Vector3.New(1,0,0)
    if self.pathData_ == nil then
        return
    end

    dt = dt * self.speedScaleFactor_

    if self.unActiveTime_ > 0 then
        self.transform_:Translate(Vector3.forward * dt * self.speed_)
        self.unActiveTime_ = self.unActiveTime_ - dt
        return
    end

    self.lastFrameLife_ = self.currentLife_
    self.currentLife_ = self.currentLife_ + dt
    
    self.stepTime_ = 0
    for i,point in ipairs(self.pathData_.pointList) do
        self.stepTime_ = self.stepTime_ + point.time
        if self.currentLife_ <= self.stepTime_ then
            self.currentStep_ = i
            break
        end

        if i == #(self.pathData_.pointList) then
            self.currentStep_ = i + 1
            --»ØÊÕÓã
            self.gameObject_:Destroy()
            return
        end
    end
    if self.lastFrameStep_ ~= self.currentStep_ then
        local tmpStep = self.lastFrameStep_
        local t1 = self.lastFrameLife_
        while true do
            tmpStep = tmpStep + 1
            if tmpStep > self.currentStep_ then
                break
            end
            local t2 = 0
            for i=1,tmpStep-1 do
                t2 = t2 + self.pathData_.pointList[i].time
            end
            local dt1 = t2 - t1
            t1 = t2
            local cnt1 = math.floor(dt1 / SECOND_ONE_FRAME)
            for i=1,cnt1 do
                self:caculateTransform(tmpStep - 1, SECOND_ONE_FRAME);
            end
            self:caculateTransform(tmpStep - 1, dt1 - SECOND_ONE_FRAME * cnt1)
        end

        local t3 = 0
        for i=1,self.currentStep_-1 do
            t3 = t3 + self.pathData_.pointList[i].time
        end

        local dt2 = self.currentLife_ - t3
        local cnt2 = math.floor(dt2 / SECOND_ONE_FRAME)
        for i=1, cnt2 do
            self:caculateTransform(self.currentStep_, SECOND_ONE_FRAME);
        end
        self:caculateTransform(self.currentStep_, dt2 - SECOND_ONE_FRAME * cnt2)
        self.lastFrameStep_ = self.currentStep_
    else
        local cnt1 = math.floor(dt / SECOND_ONE_FRAME)
        for i=1, cnt1 do
            self:caculateTransform(self.currentStep_, SECOND_ONE_FRAME);
        end
        self:caculateTransform(self.currentStep_, dt - SECOND_ONE_FRAME * cnt1)
    end

end

function Fish:caculateTransform(step, dt)
    self.oneFinePoint_ = self:caculateOneFinePoint(self.transform_.localPosition, self.transform_.eulerAngles, step, dt);
    self:setPosition(self.oneFinePoint_.position)
    self:setEulerAngles(self.oneFinePoint_.rotation)
end

function Fish:caculateOneFinePoint(startPosition,startRotation,step,dt)
	local point = {position=Vector3.zero,rotation=Vector3.zero,controlIndex = 0}
	if step < 1 or dt < 0 then 
        return point
    end
	if(step >= 1 and step <= #(self.pathData_.pointList)) then
		local rXDelta = dt * self.pathData_.pointList[step].rx / self.pathData_.pointList[step].time
		local rYDelta = dt * self.pathData_.pointList[step].ry / self.pathData_.pointList[step].time
		startRotation.x = startRotation.x + rXDelta
		startRotation.y = startRotation.y + rYDelta
	end

	step = math.min(step,#(self.pathData_.pointList));
	rotatedVec = self:rotate(Vector3.forward,startRotation.x,startRotation.y);
	local dL = rotatedVec * (self.pathData_.pointList[step].speedScale * dt * self.speed_);
		
	startPosition = startPosition + dL;

	point.position = startPosition;
	point.rotation = startRotation;
	point.controlIndex = step;

	return point;
end

function Fish:rotate(point,angleX,angleY)
    local radian = math.rad(angleX);
	local cos = math.cos(radian);
	local sin = math.sin(radian);
    local temp = Vector3.zero
	temp.x = point.x;
	temp.y = point.y * cos - point.z * sin;
	temp.z = point.y * sin + point.z * cos;

    local result = Vector3.zero
	radian = math.rad(angleY)
	cos = math.cos(radian);
	sin = math.sin(radian);
	result.x = temp.x * cos + temp.z * sin;
	result.y = temp.y;
	result.z = -sin * temp.x + temp.z * cos;

    return result;
end

return Fish