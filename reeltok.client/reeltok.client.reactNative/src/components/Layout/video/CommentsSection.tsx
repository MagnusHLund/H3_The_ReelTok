import React, { useState } from 'react'
import { Modal, View, Text, StyleSheet, FlatList } from 'react-native'
import { Ionicons } from '@expo/vector-icons'
import CustomButton from '../../input/CustomButton'
import Comment from './Comment'
import ProfilePicture from '../profile/ProfilePicture'
import CustomTextInput from '../../input/CustomTextInput'
import FontAwesome from '@expo/vector-icons/FontAwesome'

interface CommentSectionProps {
  comments: { text: string; profilePictureUrl: string; username: string }[]
  commentsAmount?: number
  showComments: boolean
  onClose: () => void
}

const CommentSection: React.FC<CommentSectionProps> = ({
  comments,
  commentsAmount,
  showComments,
  onClose,
}) => {
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

  return (
    <Modal animationType="slide" transparent={true} visible={showComments} onRequestClose={onClose}>
      <View style={styles.modalOverlay}>
        <View style={styles.modalContent}>
          <View style={styles.commentsDetailsAndControls}>
            <Text style={styles.text}> Comments: {commentsAmount} </Text>
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
            data={comments}
            renderItem={({ item }) => (
              <Comment
                commentText={item.text}
                profilePicture={<ProfilePicture pictureUrl={item.profilePictureUrl} />}
                username={item.username}
              />
            )}
          />
          <View style={styles.commentInput}>
            <CustomTextInput placeholder="comment.." widthProcentage={0.8}></CustomTextInput>
            <CustomButton
              style={styles.send}
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

export default CommentSection
