--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
local GameUIManager = require("Logic.GameUIManager")
local ViewBase = class("ViewBase",require("View.NodeBase"))

function ViewBase:ctor(obj)
    ViewBase.super.ctor(self,obj)
    self.uiManager_ = GameUIManager:getInstance()
    self.uiRootObj_ = GameUIManager:getInstance().uiRoot_
end

return ViewBase
--endregion
