function drugafunc(source, args, rawCommand)
	if isAuthorized(source) and tablelength(args) == 1 then
		local target = tonumber(args[1])
		local name = GetPlayerName(source)
		if (name ~= nil and GetPlayerName(target) ~= nil) then
			TriggerClientEvent("DGZ:ReceiveTest", target, source, name)
			TriggerClientEvent('chatMessage', source, "DRUGALYZER", {255,255,0}, "Drugalyzer test in progress...")
		end
	end
end

RegisterCommand('drugalyze', drugafunc, false)
RegisterCommand('drugalyse', drugafunc, false)
RegisterCommand('druga', drugafunc, false)

function salivafunc(source, args, rawCommand)
	if tablelength(args) == 2 then
		local name = GetPlayerName(source)
		TriggerClientEvent("DGZ:ProvideSample", source, name, args[1], args[2])
	else
		TriggerClientEvent('chatMessage', source, "DRUGALYZER", {255,255,0}, "Type '/saliva cannabis cocaine' or '/failprovide'.")
	end
end

RegisterCommand('saliva', salivafunc, false)

function failprovidefunc(source, args, rawCommand)
	local name = GetPlayerName(source)
	TriggerClientEvent("DGZ:FailProvide", source, name)
end

RegisterCommand('failprovide', failprovidefunc, false)

RegisterServerEvent("DGZ:svDrugalyze")
AddEventHandler("DGZ:svDrugalyze", function(playerid, target)
	if isAuthorized(source) then
		TriggerClientEvent("DGZ:ReceiveTest", target, playerid, GetPlayerName(playerid))
		TriggerClientEvent('chatMessage', playerid, "DRUGALYZER", {255,255,0}, "Drugalyzer test in progress...")
	end
end)

RegisterServerEvent("DGZ:SendMessage")
AddEventHandler("DGZ:SendMessage", function(targetid, message)
	TriggerClientEvent('chatMessage', targetid, "DRUGALYZER", {255,255,0}, message)
end)

function tablelength(T)
  local count = 0
  for _ in pairs(T) do count = count + 1 end
  return count
end