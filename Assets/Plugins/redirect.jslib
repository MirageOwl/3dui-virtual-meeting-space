mergeInto(LibraryManager.library, {

  RedirectMe: function () {
    if(window.location.href.indexOf("c.game1") !== -1)
    {
        window.location.replace(window.location.href.replace("c.game1", "d.middleform"));
    }
     if(window.location.href.indexOf("c.game2") !== -1)
    {
        window.location.replace(window.location.href.replace("c.game2", "d.postform"));
    }
     if(window.location.href.indexOf("d.game1") !== -1)
    {
        window.location.replace(window.location.href.replace("d.game1", "c.middleform"));
    }
     if(window.location.href.indexOf("d.game2") !== -1)
    {
        window.location.replace(window.location.href.replace("d.game2", "d.postform"));
    }
  },
});