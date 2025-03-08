import React from 'react'
import { useSelector } from 'react-redux'
import { RootState } from '../../redux/store'
import UploadVideo, { isImage } from '../layout/common/UploadVideo'
import Header from '../layout/common/Header'

const UploadVideoScreen = () => {
  const uploadedVideo = useSelector((state: RootState) => state.upload.video)
  const title = isImage(uploadedVideo.fileUri) ? 'Upload Image' : 'Upload Video'

  return (
    <>
      <Header showBackButton title={title} />
      <UploadVideo />
    </>
  )
}

export default UploadVideoScreen
