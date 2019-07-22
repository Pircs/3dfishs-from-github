require("View.MainView")
require("View.TouchEventLayer")
local IslandSelectView = class("IslandSelectView",require("View.NodeBase"))

function IslandSelectView:ctor(uimanager)
    IslandSelectView.super.ctor(self)
	self:initView()
    self.uiManager_ = uimanager
end

function IslandSelectView:initView()
    self:setObject("ui","View_IslandSelect")

	for i = 1,3 do
		local island1Btn = self:getChild("Island" .. i):GetComponent("JJButton")
		island1Btn:AddClickCallback(handler(self,self.onClickIslandBtn),island1Btn)
	end
	self.label_ = self:getChild("Label"):GetComponent("JJLabel")

    local eventListener = self:getChild("Sprite"):GetComponent("UIEventListener")
    eventListener.onClick = handler(self,self.onClickedSprite)
    eventListener.onPress = handler(self,self.onPressedSprite)

    local proxy = self.gameObject_:AddComponent(typeof(ScriptProxy))
    proxy.Table = self
end

function IslandSelectView:onUpdate(dt)
    --print(dt)
end

function IslandSelectView:onClickIslandBtn(param)
    local obj = param.gameObject
	self.label_.Text = obj.name .. "   378"
    local index = tonum( string.sub(obj.name,string.len(obj.name)) )
    self.label_.FontStyle = index
    self.label_.FontSize = 20 + 10 * index
    self.label_.FontColor = Color.New(0,1,0,0.7)

    --LeanTween.moveLocal(obj, obj.transform.localPosition + Vector3.New(100,0,0), 1):setOnCompleteCallback(handler(self,self.move1End),obj)
	--LeanTween.scale(obj, obj.transform.localScale*1.2, 1)
	--LeanTween.rotateAround(obj, Vector3.forward, -90, 1):setOnCompleteCallback(handler(self,self.rotate),"hello world!"):setEase(LeanTweenType.easeInOutElastic)
    --local a = LeanTween.value(obj, Color.red, Color.green, 1 ):setOnUpdateColor(handler(self,self.onUpdateColor))
    --a:setOnUpdateVector3(handler(self,self.onUpdateColor))

    self.uiManager_:showMainView()
    self.uiManager_:hideIslandSelectView()
end

function IslandSelectView:move1End(param)
    LeanTween.moveLocal(param, param.transform.localPosition + Vector3.New(-100,0,0), 1)
end

function IslandSelectView:rotate(param)
    print(param)
end

function IslandSelectView:onUpdateColor(val)
    self.label_.FontColor = val
end

function IslandSelectView:showScrollView()
    self.uiManager_:showView("ScrollViewTest")
end

function IslandSelectView:onClickedSprite(obj)
    print(obj.name)
end

function IslandSelectView:onPressedSprite(obj,isPressed)
    print(obj.name,isPressed)
end

function IslandSelectView:testCoin()

    LeanTween.delayedCall(2,System.Action_object(handler(self,self.jumpCoin))):setOnCompleteParam(13)
end

function IslandSelectView:jumpCoin(obj)
    print("123",obj)
end

function IslandSelectView:jumpCoin2()
    print("123")
end

return IslandSelectView