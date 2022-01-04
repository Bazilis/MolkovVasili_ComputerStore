import React from 'react'
import Message from '../Message'

const ChatWindow = (props) => {
  return (
    <div>
      {props.chat.map((m, i) => (
        <Message key={i} user={m.user} message={m.message} />
      ))}
    </div>
  )
}

export default ChatWindow
