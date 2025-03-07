import { Modal, View, Text, StyleSheet, FlatList } from 'react-native'
import useAppSelector from '../../../hooks/useAppSelector'
import CustomTextInput from '../../input/CustomTextInput'
import FontAwesome from '@expo/vector-icons/FontAwesome'
import CustomButton from '../../input/CustomButton'
import { Ionicons } from '@expo/vector-icons'
import Comment from './Comment'
import React from 'react'
import {
  selectCommentsForVideo,
  selectTotalVideoComments,
} from '../../../redux/selectors/commentsSelectors'

interface CommentsSectionProps {
  videoId: string
  onClose: () => void
}

// TODO: Fix styling on web
// TODO: On app (using physical device), ensure that the comment text input goes up with the phone's keyboard
const CommentsSection: React.FC<CommentsSectionProps> = ({ videoId, onClose }) => {
  const commentsForVideo = useAppSelector((state) => selectCommentsForVideo(state, videoId))
  const totalVideoComments = useAppSelector((state) => selectTotalVideoComments(state, videoId))

  return (
    <Modal animationType="slide" transparent={true} onRequestClose={onClose}>
      <View style={styles.modalOverlay}>
        <View style={styles.modalContent}>
          <View style={styles.commentsDetailsAndControls}>
            <Text style={styles.text}> Comments: {totalVideoComments} </Text>
            <CustomButton
              transparent={true}
              widthPercentage={0.1}
              borders={false}
              onPress={onClose}
            >
              <Ionicons name="close" size={32} color="white" />
            </CustomButton>
          </View>
          <FlatList
            contentContainerStyle={{ gap: 17.5 }}
            data={commentsForVideo}
            renderItem={({ item }) => (
              <Comment commentText={item.commentText} commenterUserId={item.userId} />
            )}
          />
          <View style={styles.commentInput}>
            <CustomTextInput placeholder="comment.." widthPercentage={0.8}></CustomTextInput>
            <CustomButton
              transparent
              onPress={() => console.log('test pepepepepe')}
              widthPercentage={0.1}
            >
              <FontAwesome name="send" size={20} color="white" />
            </CustomButton>
          </View>
        </View>
      </View>
    </Modal>
  )
}

const styles = StyleSheet.create({
  modalOverlay: {
    display: 'flex',
    justifyContent: 'flex-end',
    flexDirection: 'column',
    backgroundColor: 'transparent',
    height: '100%',
  },
  modalContent: {
    backgroundColor: '#262626',
    padding: 20,
    borderTopLeftRadius: 15,
    borderTopRightRadius: 15,
    height: '50%',
    overflow: 'scroll',
  },
  commentsDetailsAndControls: {
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    flexDirection: 'row',
    borderBottomWidth: 1,
    borderColor: 'white',
    marginBottom: 10,
  },
  text: {
    color: 'white',
    fontSize: 15,
  },
  commentInput: {
    flexDirection: 'row',
    justifyContent: 'space-evenly',
    backgroundColor: '',
    borderRadius: 5,
  },
  send: {
    left: '-20%',
  },
})

export default CommentsSection
