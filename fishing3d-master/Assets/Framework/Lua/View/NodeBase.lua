--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
local NodeBase = class("NodeBase")

function NodeBase:ctor()
    self.gameObject_ = nil
end

function NodeBase:setObject(abName,assetName)
    self.gameObject_ = ResourceManager:CreateObject(abName,assetName)
end

function NodeBase:setLayer(layerName)
    if self.gameObject_ then
        self.gameObject_.layer = LayerMask.NameToLayer(layerName)
    end
end

function NodeBase:setParent(parent)
    if self.gameObject_ and parent then
        self.gameObject_.transform.parent = parent
    end
end

function NodeBase:setPosition(x,y,z)
    if self.gameObject_ then
        if type(x) == "table" then
            self.gameObject_.transform.localPosition = x
        else
            self.gameObject_.transform.localPosition = Vector3.New(x,y,z)
        end
    end
end

function NodeBase:setScale(scale)
    if self.gameObject_ then
        self.gameObject_.transform.localScale = scale    
    end
end

function NodeBase:getPosition()
    if self.gameObject_ then
        return self.gameObject_.transform.localPosition
    end
    return nil
end

function NodeBase:setEulerAngles(x,y,z)
    if self.gameObject_ then
        self.gameObject_.transform.eulerAngles = Vector3.New(x,y,z)
    end
end

function NodeBase:setIdentity()
    if self.gameObject_ then
        self.gameObject_.transform.localPosition = Vector3.zero
        self.gameObject_.transform.localScale = Vector3.one
        self.gameObject_.transform.eulerAngles = Vector3.zero
    end
end

function NodeBase:destroy()
    if self.gameObject_ then
        self.gameObject_:Destroy()
    end
end

function NodeBase:getChild(chilename)
    if self.gameObject_ then
        return self.gameObject_.transform:FindChild(chilename)
    end
    return nil
end

function NodeBase:addChild(childTransform)
    if self.gameObject_ and childTransform then
        childTransform.parent = self.gameObject_.transform
    end
end

function NodeBase:setName(name)
    if self.gameObject_ then
        self.gameObject_.name = name
    end
end

function NodeBase:setActive(active)
    if self.gameObject_ then
        self.gameObject_:SetActive(active)
    end
end

return NodeBase
--endregion
