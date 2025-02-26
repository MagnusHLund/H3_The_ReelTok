import useAppSelector from '../../../hooks/useAppSelector'
import ProfilePicture from '../profile/ProfilePicture'
import { StyleSheet, Text, View } from 'react-native'

interface CommentProps {
  commenterUserId: string
  commentText: string
}

const Comment: React.FC<CommentProps> = ({ commenterUserId, commentText }) => {
  const commenter = useAppSelector((state) =>
    state.users.users.find((user) => user.userId === commenterUserId)
  )

  if (commenter === undefined) {
    return
  }

  return (
    <View style={styles.commentContainer}>
      <ProfilePicture pictureUrl={commenter.profilePictureUrl} />
      <View style={styles.commentDetails}>
        <Text style={styles.usernameText}>{commenter.username}</Text>
        <Text style={styles.commentText}>{commentText}</Text>
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  commentContainer: {
    display: 'flex',
    justifyContent: 'flex-start',
    flexDirection: 'row',
    gap: 10,
  },
  commentPicture: {
    display: 'flex',
    justifyContent: 'flex-start',
  },
  commentDetails: {
    gap: 3,
  },
  usernameText: {
    color: 'white',
    fontSize: 12.5,
  },
  commentText: {
    color: 'white',
    fontSize: 14.5,
  },
})

export default Comment
