import { BaseTrack } from "./baseTrack";
import { TrackUser } from "./trackUser";

export interface Track extends BaseTrack {
    users?: TrackUser[];
    likes: number;
    dislikes: number;
    likedByUser?: boolean;
}