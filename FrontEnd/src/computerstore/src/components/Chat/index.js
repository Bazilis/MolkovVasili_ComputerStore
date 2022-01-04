import React from 'react'
import { HubConnectionBuilder } from '@microsoft/signalr'
import ChatInput from './ChatInput'
import ChatWindow from './ChatWindow'

const Host = 'http://localhost:25648'

const Chat = () => {
  const [connection, setConnection] = React.useState(null)
  const [chat, setChat] = React.useState([])
  const latestChat = React.useRef(null)

  latestChat.current = chat

  React.useEffect(() => {
    const connection = new HubConnectionBuilder()
      .withUrl(`${Host}/hubs/chat`)
      .withAutomaticReconnect()
      .build()

    setConnection(connection)
  }, [])

  React.useEffect(() => {
    if (connection) {
      connection
        .start()
        .then((result) => {
          console.log('Connected')
          connection.on('ReceiveMessage', (message) => {
            const updatedChat = [...latestChat.current]
            // const updatedChat =[]
            // latestChat.current.forEach(element => {
            //   updatedChat.push(element)
            // });
            updatedChat.push(message)
            setChat(updatedChat)
          })
        })
        .catch((error) => {
          console.log('Connection error', error)
        })
    }
  }, [connection])

  const sendMessage = async (user, message) => {
    const chatMessage = { user, message }
    if (connection && connection.connectionStarted) {
      try {
        await connection.send('SendMessage', chatMessage)
      } catch (error) {
        console.log(error)
      }
    }
  }

  return (
    <div>
      <ChatInput sendMessage={sendMessage} />
      <br />
      <ChatWindow chat={chat} />
    </div>
  )
}

export default Chat
