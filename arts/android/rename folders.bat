@echo off

FOR /D %%D IN ("C:\Projects\mrrondon-app\arts\android") DO CALL :RENAME %%D

:RENAME
SET CRITERIA=mipmap-*
FOR /D %%R IN (%1%CRITERIA%) DO RENAME %%R "drawable-*"