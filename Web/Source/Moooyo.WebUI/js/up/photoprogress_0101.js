function FileProgress(a, b) {
    var i = document;
    this.fileProgressID = a.id;
    this.opacity = 100;
    this.height = 0;
    this.fileProgressWrapper = i.getElementById(this.fileProgressID);
    if (!this.fileProgressWrapper) {
        this.fileProgressWrapper = i.createElement("div");
        this.fileProgressWrapper.className = "progressWrapper";
        this.fileProgressWrapper.id = this.fileProgressID;
        this.fileProgressElement = i.createElement("div");
        this.fileProgressElement.className = "progressContainer";
        var h = i.createElement("a");
        h.href = "#";
        h.className = "progressCancel";
        h.appendChild(i.createTextNode("x"));
        var c = i.createElement("span");
        c.className = "progressName";
        c.appendChild(i.createTextNode(a.name));
        var f = i.createElement("span");
        f.className = "progressSize";
        f.appendChild(
            i.createTextNode(a.size / 1024 < 1024 ? (a.size / 1024).toFixed(1) + " KB" : (a.size / 1024 / 1024).toFixed(2) + " MB")
            );
        var d = i.createElement("div");
        d.className = "progressBarBack";
        var g = i.createElement("div");
        g.className = "progressBarInProgress";
        d.appendChild(g);
        var e = i.createElement("div");
        e.className = "progressBarStatus";
        e.innerHTML = "&nbsp;";
        this.fileProgressElement.appendChild(h);
        this.fileProgressElement.appendChild(c);
        this.fileProgressElement.appendChild(e);
        this.fileProgressElement.appendChild(d);
        this.fileProgressElement.appendChild(f);
        this.fileProgressWrapper.appendChild(this.fileProgressElement);
        i.getElementById(b).appendChild(this.fileProgressWrapper)
    }
    else {
        this.fileProgressElement = this.fileProgressWrapper.firstChild
    }
    this.height = this.fileProgressWrapper.offsetHeight
}

FileProgress.prototype.setProgress = function (a) {
    this.fileProgressElement.className = "progressContainer green";
    this.fileProgressElement.childNodes[3].childNodes[0].className = "progressBarInProgress";
    this.fileProgressElement.childNodes[3].childNodes[0].style.width = a + "%";

};
FileProgress.prototype.setComplete = function () {
    this.fileProgressElement.className = "progressContainer blue";
    this.fileProgressElement.childNodes[3].className = "progressBarComplete";
    this.fileProgressElement.childNodes[3].style.width = ""
};
FileProgress.prototype.setError = function () {
    this.fileProgressElement.className = "progressContainer red";
    this.fileProgressElement.childNodes[3].className = "progressBarError";
    this.fileProgressElement.childNodes[3].style.width = ""
};
FileProgress.prototype.setCancelled = function () {
    this.fileProgressElement.className = "progressContainer";
    if (this.fileProgressElement.childNodes[3]) {
        this.fileProgressElement.childNodes[3].className = "progressBarError";
        this.fileProgressElement.childNodes[3].style.width = ""
    }
    var a = this; setTimeout(function () { a.disappear() }, 200)
};
FileProgress.prototype.setStatus = function (a) {
    this.fileProgressElement.childNodes[2].innerHTML = a
};
FileProgress.prototype.removeDelIcon = function () {
    $(this.fileProgressElement.childNodes[0]).remove()
};
FileProgress.prototype.toggleCancel = function (b, c, d) {
    this.fileProgressElement.childNodes[0].style.visibility = b ? "visible" : "hidden";
    if (c) {
        if (d) {
            this.fileProgressElement.childNodes[0].onclick = function () { d() } 
        }
        else {
            var a = this.fileProgressID;
            this.fileProgressElement.childNodes[0].onclick = function () {
                c.cancelUpload(a);
                return false
            } 
        } 
    } 
};
FileProgress.prototype.disappear = function () {
    var f = 15, c = 4, b = 30;
    if (this.opacity > 0) {
        this.opacity -= f;
        if (this.opacity < 0) {
            this.opacity = 0
        }
        if (this.fileProgressWrapper.filters) {
            try {
                this.fileProgressWrapper.filters.item("DXImageTransform.Microsoft.Alpha").opacity = this.opacity
            }
            catch (d) {
                this.fileProgressWrapper.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity=" + this.opacity + ")"
            } 
        }
        else {
            this.fileProgressWrapper.style.opacity = this.opacity / 100
        } 
    }
    if (this.height > 0) {
        this.height -= c;
        if (this.height < 0) {
            this.height = 0
        }
        this.fileProgressWrapper.style.height = this.height + "px"
    }
    if (this.height > 0 || this.opacity > 0) {
        var a = this; setTimeout(function () { a.disappear() }, b)
    }
    else {
        $(this.fileProgressWrapper).remove();
        if (!$("#fsUploadProgress").children().length) {
            $(".upload-list, .opt-btns").addClass("hidden")
        } 
    } 
};
