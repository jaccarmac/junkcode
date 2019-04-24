(ns app.main
  #_(:require ["/App" :default App])
  (:require ["svelte/compiler" :as svelte]))

#_(new App {:target js/document.body})

(defn main! []
  (.log js/console (.-VERSION svelte)))
