ScrollViewTest = class("ScrollViewTest",require("View.ViewBase"))

function ScrollViewTest:ctor(gameObject)
	ScrollViewTest.super.ctor(self)
	self.gameObject = gameObject
    self.gameObject.name = "ScrollViewTest"
	self.transform = gameObject.transform
    self.transform.parent = self.uiRootObj_.transform
    self.transform.localPosition = Vector3.New(0,0,0)
    self.transform.localScale = Vector3.one
	self:initView()
end

function ScrollViewTest:initView()
    local scrollview = self.transform:FindChild("ScrollView")
	local grid = scrollview:FindChild("Grid"):GetComponent("UIGrid")
    local itemtemplate = scrollview:FindChild("ItemTemplate").gameObject
    for i = 0,10 do
        local item = GameObject.Instantiate(itemtemplate)
        grid:AddChild(item.transform)
        item.transform.localScale = Vector3.one
    end
    itemtemplate:SetActive(false)

    local closebtn = self.transform:FindChild("Btn_Close"):GetComponent("JJButton")
    closebtn:AddClickCallback(handler(self,self.onClickCloseBtn),nil)
end

function ScrollViewTest:onClickCloseBtn(obj)
    self.uiManager_:hideView("ScrollViewTest")
end