import React from 'react'

const ChatInput = (props) => {
  const [user, setUser] = React.useState(null)
  const [message, setMessage] = React.useState(null)

  const onSubmit = (e) => {
    e.preventDefault()
    const isUserProvided = user && user !== ''
    const isMessageProvided = message && message !== ''
    if (isUserProvided && isMessageProvided) {
      props.sendMessage(user, message)
    }
  }
  const onUserUpdate = (e) => {
    setUser(e.target.value)
  }
  const onMessageUpdate = (e) => {
    setMessage(e.target.value)
  }

  return (
    <form onSubmit={onSubmit}>
      <input
        id="user"
        name="user"
        value={user}
        onChange={onUserUpdate}
        placeholder="user"
      />
      <br />
      <input
        id="message"
        name="message"
        value={message}
        onChange={onMessageUpdate}
        placeholder="message"
      />
      <button type="submit">Submit</button>
    </form>
  )
}
export default ChatInput
