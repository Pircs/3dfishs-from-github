local FishingGameTable = class("FishingGameTable")
FishingGameTable.records = {}

function FishingGameTable:addValue(index,column,values)
end

function FishingGameTable:addRecord(record)
     if record ~= nil then
        self.records[#(self.records)+1] = record
    end
end

function FishingGameTable:getRecordCnt()
    return #(self.records)
end

function FishingGameTable:getRecords()
    return self.records
end

--[[
鱼表结构
]]
FishingGameTable.Table_Fish = FishingGameTable:new()
local Table_Fish = FishingGameTable.Table_Fish
Table_Fish.records = {}
local Table_Fish_Field = {ID = 1,
		CHINESENAME = 2,
		NAME = 3,
		MOVE = 4,
		DEAD = 5,
		HIT = 6,
		SPEED = 7,
		POINT = 8,
		Width = 9,
		Height = 10,
        SCALE = 11
        }
function Table_Fish:addValue(index,column,values)
    local record = nil
    if #(self.records) < index then
        record = {}
        self:addRecord(record)
    else
        record = self.records[index]
    end
    if column == Table_Fish_Field.ID then
        record.id = toint(values)
    elseif column == Table_Fish_Field.CHINESENAME then
        record.chinesename = values
    elseif column == Table_Fish_Field.NAME then
        record.name = values
    elseif column == Table_Fish_Field.MOVE then
        record.move = values
    elseif column == Table_Fish_Field.DEAD then
        record.dead = values
    elseif column == Table_Fish_Field.HIT then
        record.hit = values
    elseif column == Table_Fish_Field.SPEED then
        record.speed = toint(values)
    elseif column == Table_Fish_Field.POINT then
        record.point = toint(values)
    elseif column == Table_Fish_Field.Width then
        record.width = tonum(values)
    elseif column == Table_Fish_Field.Height then
        record.height = tonum(values)
    elseif column == Table_Fish_Field.SCALE then
        record.scale = tonum(values)
    end
end

function Table_Fish:getRecordById(id)
    for key,value in pairs(self.records) do
        if id == value.id then
            return value
        end
    end
    return nil
end

function Table_Fish:getRecordByName(name)
    for key,value in pairs(self.records) do
        if name == value.name then
            return value
        end
    end
    return nil
end

--[[
大炮武器表结构
]]
FishingGameTable.Table_Weapon = FishingGameTable:new()
local Table_Weapon = FishingGameTable.Table_Weapon
Table_Weapon.records = {}
local Table_Weapon_Field = {
		LEVEL = 1,
		CANNONNAME = 2,
		CANNONTYPE = 3,
		BULLETSPEED = 4,
		BULLETTYPE = 5,
		HASBOMBEFFECT = 6,
		BULLETBUMPNUM = 7,
		CANNONANIM = 8,
		BULLETANIM = 9,
		BOMBANIM = 10,
		FIRESOUND = 11,
		BOMBSOUND = 12,
		ANCHOR = 13,
        CANNONFRAMEBEGIN = 14,
        CANNONFRAMELENGTH = 15,
        BULLETFRAMEBEGIN = 16,
        BULLETFRAMELENGTH = 17,
        BOMBFRAMEBEGIN = 18,
        BOMBFRAMELENGTH = 19,
        DAMAGEVALUE = 20,
        BOMBRADIUS = 21,
        DIZUO = 22,
        FIREANIM = 23,
        FIREANIMFRAMELENGTH = 24,
        FIREINTERVAL = 25,
        BOMBSCALE = 26,
        CANNONSCALE = 27,
        BULLETSCALE = 28,
        BULLETSPARKLEANIM = 29,
        SPARKLEFRAMELENGTH = 30,
        MAXCAPFISHCOUNT = 31
	}

function Table_Weapon:addValue(index,column,values)
    local record = nil
    if #(self.records) < index then
        record = {}
        self:addRecord(record)
    else
        record = self.records[index]
    end
    if string.len(values) <= 0 then
        return
    end

    if column ==  Table_Weapon_Field.LEVEL then
        record.level = toint(values)
    elseif column ==  Table_Weapon_Field.CANNONNAME then
        record.cannonname = values
    elseif column ==  Table_Weapon_Field.CANNONTYPE then
        record.cannontype = toint(values)
    elseif column ==  Table_Weapon_Field.BULLETSPEED then
        record.bulletspeed = toint(values)
    elseif column ==  Table_Weapon_Field.BULLETTYPE then
        record.bullettype = toint(values)
    elseif column ==  Table_Weapon_Field.HASBOMBEFFECT then
        record.hasbombeffect = toint(values)
    elseif column ==  Table_Weapon_Field.BULLETBUMPNUM then
        record.bulletbumpnum = toint(values)
    elseif column ==  Table_Weapon_Field.CANNONANIM then
        record.cannonanim = values
    elseif column ==  Table_Weapon_Field.BULLETANIM then
        record.bulletanim = values
    elseif column ==  Table_Weapon_Field.BOMBANIM then
        record.bombanim = values
    elseif column ==  Table_Weapon_Field.FIRESOUND then
        record.firesound = values
    elseif column ==  Table_Weapon_Field.BOMBSOUND then
        record.bombsound = values
    elseif column ==  Table_Weapon_Field.ANCHOR then
        record.anchor = tonum(values)
    elseif column == Table_Weapon_Field.CANNONFRAMEBEGIN then
        record.cannonFrameBegin = tonum(values)
    elseif column == Table_Weapon_Field.CANNONFRAMELENGTH then
        record.cannonFrameLength = tonum(values)
    elseif column == Table_Weapon_Field.BULLETFRAMEBEGIN then
        record.bulletFrameBegin = tonum(values)
    elseif column == Table_Weapon_Field.BULLETFRAMELENGTH then
        record.bulletFrameLength = tonum(values)
    elseif column == Table_Weapon_Field.BOMBFRAMEBEGIN then
        record.bombFrameBegin = tonum(values)
    elseif column == Table_Weapon_Field.BOMBFRAMELENGTH then
        record.bombFrameLength = tonum(values)
    elseif column == Table_Weapon_Field.DAMAGEVALUE then
        record.damageValue = tonum(values)
    elseif column == Table_Weapon_Field.BOMBRADIUS then
        record.bombRadius = tonum(values)
    elseif column == Table_Weapon_Field.DIZUO then
        record.dizuo = values
    elseif column == Table_Weapon_Field.FIREANIM then
        record.fireFlash = values
    elseif column == Table_Weapon_Field.FIREANIMFRAMELENGTH then
        record.fireFrameLength = values
    elseif column == Table_Weapon_Field.FIREINTERVAL then
        record.fireInterval = tonum(values)
    elseif column == Table_Weapon_Field.BOMBSCALE then
        record.bombScale = tonum(values)
    elseif column == Table_Weapon_Field.CANNONSCALE then
        record.cannonScale = tonum(values)
    elseif column == Table_Weapon_Field.BULLETSCALE then
        record.bulletScale = tonum(values)
    elseif column == Table_Weapon_Field.BULLETSPARKLEANIM then
        record.sparkleAnim = values
    elseif column == Table_Weapon_Field.SPARKLEFRAMELENGTH then
        record.sparkleFrameLength = tonum(values)
    elseif column == Table_Weapon_Field.MAXCAPFISHCOUNT then
        record.maxcapfishcount = tonum(values)
    end
end

function Table_Weapon:getRecordByWeaponLevel(iWeaponLevel)
    for key,value in pairs(self.records) do
        if iWeaponLevel == value.level then
            return value
        end
    end
    return nil
end

--[[
路径AI表结构
]]

FishingGameTable.Table_AI = FishingGameTable:new()
local Table_AI = FishingGameTable.Table_AI
Table_AI.records = {}
Table_AI.aiMap = {}
Table_AI.mAiId = -1
local Table_AI_Field = {
        TimeId = 1,
        RotateDegree = 2,
        SpeedMulti = 3,
        RotateFactor = 4,
        SpeedFactor = 5
        }

function Table_AI:addValue(index,column,values)
    local record = nil
    if #(self.records) < index then
        record = {}
        self:addRecord(record)
    else
        record = self.records[index]
    end

    if column == Table_AI_Field.TimeId then
        record.timeid = toint(values)
        if record.timeid == 0 then
            self.mAiId = self.mAiId + 1
        end
    elseif column == Table_AI_Field.RotateDegree then
        record.rotatedegree = toint(values)
    elseif column == Table_AI_Field.SpeedMulti then
        record.speedmulti = tonum(values)
        if self.aiMap[self.mAiId] == nil then
            self.aiMap[self.mAiId] = {}
        end
        self.aiMap[self.mAiId][#(self.aiMap[self.mAiId]) + 1] = record
    elseif column == Table_AI_Field.RotateFactor then
        record.rotateFactor = tonum(values)
    elseif column == Table_AI_Field.SpeedFactor then
        record.speedFactor = tonum(values)
    end
end

function Table_AI:getAiRecordsByAiId(aiid)
    for key,value in pairs(self.aiMap) do
        if key == aiid then
            return self.aiMap[key]
        end
    end
    return nil
end

--物品道具表
FishingGameTable.Table_DropItem = FishingGameTable:new()
local Table_DropItem = FishingGameTable.Table_DropItem
Table_DropItem.records = {}
local  Table_DropItem_Field =
	{
		ID = 1,
		TYPE =2,
		NAME = 3,
		ICON = 4,
        ATTACHANIM = 5,
        SERVERID = 6,
        DESCRIPTION = 7
	}
function Table_DropItem:addValue(index,column,values)
    local record = nil
    if #(self.records) < index then
        record = {}
        self:addRecord(record)
    else
        record = self.records[index]
    end

    if column == Table_DropItem_Field.ID then
        record.id = tonum(values)
    elseif column == Table_DropItem_Field.TYPE then
        record.type = tonum(values)
    elseif column == Table_DropItem_Field.NAME then
        record.name = values
    elseif column == Table_DropItem_Field.ICON then
        record.icon = values
    elseif column == Table_DropItem_Field.ATTACHANIM then
        record.attachAnim = values
    elseif column == Table_DropItem_Field.SERVERID then
        if values ~= nil and string.len(values) > 0 then
            record.serverDomainId = toint(values)
        end
    elseif column == Table_DropItem_Field.DESCRIPTION then
        if values ~= nil and string.len(values) > 0 then
            record.description = values
        end
    end
end

function Table_DropItem:getRecordById(id)
    for key,value in pairs(self.records) do
        if value.id == id then
            return value
        end
    end
    return nil
end

FishingGameTable.Table_Achieve = FishingGameTable:new()
local Table_Achieve = FishingGameTable.Table_Achieve
Table_Achieve.records = {}
local Table_Achieve_Field = {
    ID = 1,
	TYPE = 2,
	DOMAINID = 3,
	NAME = 4,
	DESC = 5,
	CONDITION = 6,
	SCORE = 7,
	STAR = 8,
	ICON = 9
}
function Table_Achieve:addValue(index,column,values)
    local record = nil
    if #(self.records) < index then
        record = {}
        self:addRecord(record)
    else
        record = self.records[index]
    end

    if column == Table_Achieve_Field.ID then
        record.id = tonum(values)
    elseif column == Table_Achieve_Field.TYPE then
        record.type = tonum(values)
    elseif column == Table_Achieve_Field.DOMAINID then
        if string.len(values) > 0 then
            record.domainid = tonum(values)
        end
    elseif column == Table_Achieve_Field.NAME then
        record.name = values
    elseif column == Table_Achieve_Field.DESC then
        record.desc = values
    elseif column == Table_Achieve_Field.CONDITION then
        record.condition = string.split(values,'/')
    elseif column == Table_Achieve_Field.SCORE then
        record.score = string.split(values,'/')
    elseif column == Table_Achieve_Field.STAR then
        record.star = toint(values)
    elseif column == Table_Achieve_Field.ICON then
        record.icon = string.rtrim(values)
    end
end

function Table_Achieve:getRecordById(id)
    for key,value in pairs(self.records) do
        if value.id == id then
            return value
        end
    end
    return nil
end

--物品道具表
FishingGameTable.Table_AchieveAward = FishingGameTable:new()
local Table_AchieveAward = FishingGameTable.Table_AchieveAward
Table_AchieveAward.records = {}
local Table_AchieveAward_Field = {
    EVENTID = 1,
    ACHIEVEID = 2,
    STARLEVEL = 3,
    AWARDIDS = 4,
    AWARDCOUNT = 5,
    FANGANID = 6
}
function Table_AchieveAward:addValue(index,column,values)
    if string.len(values) == 0 then
        return
    end
    local record = nil
    if #(self.records) < index then
        record = {}
        self:addRecord(record)
    else
        record = self.records[index]
    end

    if column == Table_AchieveAward_Field.EVENTID then
        record.eventid = toint(values)
    elseif column == Table_AchieveAward_Field.ACHIEVEID then
        record.achieveid = toint(values)
    elseif column == Table_AchieveAward_Field.STARLEVEL then
        record.starlevel = toint(values)
    elseif column == Table_AchieveAward_Field.AWARDIDS then
        record.awardids = string.split(values,",")
    elseif column == Table_AchieveAward_Field.AWARDCOUNT then
        record.awardcount = string.split(values,",")
    elseif column == Table_AchieveAward_Field.FANGANID then
        record.fanganid = toint(values)
    end
end

--通过事件id，返回某一行数据
function Table_AchieveAward:getRecordByEventId(eventid)
    for key,value in pairs(self.records) do
        if value.eventid == eventid then
            return value
        end
    end
    return nil
end

--返回奖励物品id数组
function Table_AchieveAward:getAwardIdsByEventId(eventid)
    for key,value in pairs(self.records) do
        if value.eventid == eventid then
            return value.awardids
        end
    end
    return nil
end

--返回奖励物品数量数组
function Table_AchieveAward:getAwardCountByEventId(eventid)
    for key,value in pairs(self.records) do
        if value.eventid == eventid then
            return value.awardcount
        end
    end
    return nil
end

--返回奖励物品id和物品数量
function Table_AchieveAward:getAwardDataByEventId(eventid)
    for key,value in pairs(self.records) do
        if value.eventid == eventid then
            return value.awardids,value.awardcount
        end
    end
    return nil,nil
end

--每日任务表
FishingGameTable.Table_PerTask = FishingGameTable:new()
local Table_PerTask = FishingGameTable.Table_PerTask
Table_PerTask.records = {}
local Table_PerTask_Field =
    {
        EVENT_ID = 1,
        DESC = 2,
        NEED_NUM = 3,
        ICON = 4,
        NUM_EVENT_ID = 5
    }
function Table_PerTask:addValue(index,column,values)
    local record = nil
    if #(self.records) < index then
        record = {}
        self:addRecord(record)
    else
        record = self.records[index]
    end
    if column == Table_PerTask_Field.EVENT_ID then
        record.eventId = toint(values)
    elseif column == Table_PerTask_Field.DESC then
        record.desc = values
    elseif column == Table_PerTask_Field.NEED_NUM then
        record.needNum = toint(values)
    elseif column == Table_PerTask_Field.ICON then
        record.icon = values
    elseif column == Table_PerTask_Field.NUM_EVENT_ID then
        record.numEventId = toint(values)
    end
end

function Table_PerTask:getRecordById(fmsId)
    for key,value in pairs(self.records) do
        if value.eventId == fmsId then
            return value
        end
    end
    return nil
end

--每日任务奖励表
FishingGameTable.Table_PerTaskAward = FishingGameTable:new()
local Table_PerTaskAward = FishingGameTable.Table_PerTaskAward
Table_PerTaskAward.records = {}
local Table_PerTaskAward_Field =
    {
        RESULT_ID = 1,
        ITEM_ID = 2,
        COUNT = 3
    }
function Table_PerTaskAward:addValue(index,column,values)
    local record = nil
    if #(self.records) < index then
        record = {}
        self:addRecord(record)
    else
        record = self.records[index]
    end
    if column == Table_PerTaskAward_Field.RESULT_ID then
        record.resultId = toint(values)
    elseif column == Table_PerTaskAward_Field.ITEM_ID then
        record.itemId = toint(values)
    elseif column == Table_PerTaskAward_Field.COUNT then
        record.count = toint(values)
    end
end

function Table_PerTaskAward:getRecordById(resultId)
    for key,value in pairs(self.records) do
        if value.resultId == resultId then
            return value
        end
    end
    return nil
end
--签到任务奖励表
FishingGameTable.Table_SignUp = FishingGameTable:new()
local Table_SignUp = FishingGameTable.Table_SignUp
Table_SignUp.records = {}
local Table_SignUp_Field =
    {
        DAY = 1, --签到天数
        SCHEME_A = 2, --奖励方案A --4,2/5,2/6,2 物品id,数量/物品id,数量/物品id,数量
        SCHEME_B = 3, --奖励方案B
        SCHEME_C = 4  --奖励方案C
    }
function Table_SignUp:addValue(index,column,values)
    local record = nil
    if #(self.records) < index then
        record = {}
        self:addRecord(record)
    else
        record = self.records[index]
    end
    if column == Table_SignUp_Field.DAY then
        record.day = tonum(values)
    elseif column == Table_SignUp_Field.SCHEME_A then
        record.schemeA = values
        self:parseChemeData(values)
    elseif column == Table_SignUp_Field.SCHEME_B then
        record.schemeB = values
        self:parseChemeData(values)
    elseif column == Table_SignUp_Field.SCHEME_C then
        record.schemeC = values
        self:parseChemeData(values)
    end
end

--解析签到奖励方案数据
function Table_SignUp:parseChemeData(data)
    if type(data) ~= "string" then
        return nil
    end
    local itemIdList = {} --物品id列表
    local itemNumList = {} --对应物品数量列表

    local values = string.split(data,'/')
    for var in pairs(values) do
        local str = string.rtrim(values[var])
        local info = string.split(str,',')
        itemIdList[var] = tonum(info[1])
        itemNumList[var] = tonum(info[2])
    end
    return itemIdList,itemNumList
end

function Table_SignUp:getRecordById(day)
    for key,value in pairs(self.records) do
        if value.day == day then
            return value
        end
    end
    return nil
end
--endregion


return FishingGameTable