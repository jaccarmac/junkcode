(ns extract
  (:require ["pdf.js-extract" :refer (PDFExtract)]
            ["util" :as util]))

(defn main [& cli-args]
  (.extract (new PDFExtract)
            (first cli-args)
            #js{}
            #(if %1
               (js/console.log %1)
               (js/console.log (util/inspect %2 false nil true)))))
