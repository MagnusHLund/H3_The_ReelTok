import React, { useState } from 'react';
import { Modal, View, Text, StyleSheet, FlatList } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import CustomButton from '../../input/CustomButton';
import Comment from './Comment';
import ProfilePicture from '../profile/ProfilePicture';
import CustomTextInput from '../../input/CustomTextInput';

interface CommentSectionProps {
  comments: { text: string; profilePictureUrl: string; username: string }[];
  commentsAmount?: number,
  showComments: boolean,
  onClose: () => void,
}

const CommentSection: React.FC<CommentSectionProps> = ({ comments, commentsAmount, showComments, onClose }) => {
  
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
        marginBottom: 10
      }, 
      text: {
        color: 'white',
        fontSize: 15,
      }
  });
  
  return (
    <>
      <Modal
        animationType="slide"
        transparent={true}
        visible={showComments}
        onRequestClose={onClose}
      >
        <View style={styles.modalOverlay}> 
          <View style={styles.modalContent}>
            <View style={styles.commentsDetailsAndControls}>
              <Text style={styles.text}> Comments: {commentsAmount} </Text>
              <CustomButton transparent={true}  borders={false} onPress={onClose}>
                <Ionicons name="close" size={32} color="white" />
              </CustomButton>
            </View>
            <FlatList
            contentContainerStyle={{ gap: 17.5 }}
            data={comments} 
            keyExtractor={(index) => index.toString()} 
            renderItem={({ item }) => ( 
              <Comment 
                commentText={item.text} 
                profilePicture={<ProfilePicture pictureUrl={item.profilePictureUrl} />} 
                username={item.username} 
              />
            )}
          />
          </View>
        </View>
      </Modal>
    </>
  )
};

export default CommentSection;
