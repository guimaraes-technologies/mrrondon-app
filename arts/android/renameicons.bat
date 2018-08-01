@echo off
for /D %%d in (*) do (
   ren "%%d\iconn.png" "icon.png"
)