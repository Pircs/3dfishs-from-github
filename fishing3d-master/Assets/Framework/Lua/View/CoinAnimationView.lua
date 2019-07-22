--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
local CoinAnimationView = class("CoinAnimationView")

function CoinAnimationView:ctor()
    self:initView()
end

function CoinAnimationView:initView()
    self.gameObject_ = ResourceManager:CreateObject("ui","View_CoinAnimation")
    self.transform_ = self.gameObject_.transform
    local GameUIManager = require("Logic.GameUIManager")
    self.transform_.parent = GameUIManager:getInstance().uiRoot_.transform
    self.transform_.localPosition = Vector3.zero
    self.transform_.localScale = Vector3.one
    self.transform_.eulerAngles = Vector3.zero
end

function CoinAnimationView:popCoin(position)
    local coin = ResourceManager:CreateObject("coins","GoldCoin")
    coin.layer = LayerMask.NameToLayer("UI")
    coin.transform.parent = self.transform_
    coin.transform.localScale = Vector3.zero
    coin.transform.localPosition = position

    local localPositionY = coin.transform.localPosition.y
    LeanTween.moveLocalY(coin, localPositionY + 100, 0.2); 
    LeanTween.scale(coin,Vector3.New(1,1,1),0.4);
    LeanTween.moveLocalY(coin, localPositionY, 0.2):setDelay(0.2);
    LeanTween.moveLocalY(coin, localPositionY + 50, 0.15):setDelay(0.4);
    LeanTween.moveLocalY(coin, localPositionY, 0.15):setDelay(0.55):setOnComplete(System.Action_object(handler(self,self.coinFlyToPlayer))):setOnCompleteParam(coin)
end

function CoinAnimationView:coinFlyToPlayer(coin)
    LeanTween.moveLocal(coin, Vector3.New(0, -360, 0), 0.5):setDelay(0.2):setOnComplete(System.Action_object(handler(self,self.coinFlyToPlayerCallback))):setOnCompleteParam(coin)
end

function CoinAnimationView:coinFlyToPlayerCallback(coin)
    coin:Destroy()
end

return CoinAnimationView
--endregion
